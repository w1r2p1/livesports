using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
namespace livesports
{
    public static class Details
    {
        private static IWebDriver webDriver = Driver.webDriver;

        public static void GetDetails()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));

            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(15));

            try
            {
                webDriver.SwitchTo().Window(webDriver.WindowHandles[1].ToString());
                string current_window = webDriver.CurrentWindowHandle;

                IWebElement state = Base.IsVisible(webDriver, By.ClassName("mstat"));
                if (state.Text.Length < 1)
                {
                    Console.WriteLine("Mecz sie jeszcze nie zaczal.\n");
                    webDriver.Close();
                    Base.Refresh();
                    Base.Getter();
                }
                else
                {
                    IWebElement home = Base.IsVisible(webDriver, By.ClassName("tname-home"));
                    IWebElement result = Base.IsVisible(webDriver, By.ClassName("current-result"));
                    IWebElement away = Base.IsVisible(webDriver, By.ClassName("tname-away"));

                    Console.Clear();
                    Console.WriteLine(string.Format("{0} - {1} {2} ({3})\n", home.Text, away.Text, result.Text, state.Text));
                    
                    wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("table#parts tr")));

                    IList<IWebElement> rows = Base.AreVisible(webDriver, By.CssSelector("table#parts tr"));
                    if (rows.Count > 0)
                    {
                        for (int i = 0; i <= rows.Count - 1; i++)
                        {
                            if (rows[i].Text.Length > 3)
                            {
                                Console.WriteLine(rows[i].Text.Replace("\r\n", " "));
                                //Console.WriteLine(CheckTime(rows.ElementAt(i)) + AddEvent(rows.ElementAt(i)));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Blad: nie pobralo wydarzen.");
                    }
                    Console.WriteLine("___________________________");
                    webDriver.Close();
                    Base.Refresh();
                    Base.Getter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Details error " + ex.Message.ToString());
            }
        }

        private static string AddEvent(IWebElement element)
        {
            switch (element.FindElement(By.TagName("span")).GetAttribute("class"))
            {
                case "icon y-card":
                    return " YELLOW CARD " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon yr-card":
                    return " YELLOW/RED " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon penalty-missed":
                    return " MISSED PENALTY " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon soccer-ball":
                    return " (GOAL) " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon r-card":
                    return " RED CARD " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon soccer-ball-own":
                    return " (OWN GOAL) " + element.FindElement(By.ClassName("participant-name")).Text;
                case "icon substitution-in":
                    return " SUBSTITUTION " + element.FindElement(By.ClassName("substitution-in-name")).Text + " <FOR> "
                                       + element.FindElement(By.ClassName("substitution-out-name")).Text;
                default:
                    return "";
            }
        }
        private static string CheckTime(IWebElement element)
        {
            int i;
            string time;
            switch (element.FindElement(By.CssSelector("div.wrapper div")).GetAttribute("class"))
            {
                case "time-box":
                    i = element.Text.IndexOf("'");
                    time = element.Text.Remove(i + 1);
                    return time;
                case "time-box-wide":
                    i = element.Text.IndexOf("'");
                    time = element.Text.Remove(i + 1);
                    return time;
                default:
                    return "";
            }
        }
    }
}
