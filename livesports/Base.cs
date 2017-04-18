using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace livesports
{
    public class Base
    {

        private static IWebDriver webDriver = Driver.webDriver;
        public static string homewindow;

        public static IWebElement IsVisible(IWebDriver driver, By element)
        {
            try {
                WebDriverWait waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                return waiter.Until(ExpectedConditions.ElementIsVisible(element));
            }
            catch {
                throw new Exception("Nie mozna odnalesc " + element);
            }
        }
        public static IList<IWebElement> AreVisible(IWebDriver driver, By element)
        {
            try {
                WebDriverWait waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                waiter.Until(d => d.FindElement(element));
                return waiter.Until(d => d.FindElements(element));
            }
            catch {
                return null;
            }
        }
        internal static void showSports()
        {
            Console.WriteLine("1 - Pilka nozna\n2 - Tenis\n3 - Hokej\n4 - Koszykówka\n5 - Siatkówka\n6 - Baseball");
        }
        internal static void Getter()
        {
            showSports();
            Console.Write("Podaj numer lub nazwe dyscypliny: ");
            string sportsname = Console.ReadLine();

            switch (sportsname)
            {
                case "1":
                    sportsname = "Piłka nożna";
                    break;
                case "2":
                    sportsname = "Tenis";
                    break;
                case "3":
                    sportsname = "Hokej";
                    break;
                case "4":
                    sportsname = "Koszykówka";
                    break;
                case "5":
                    sportsname = "Siatkówka";
                    break;
                case "6":
                    sportsname = "Baseball";
                    break;
                default:
                    break;
            }
            
            Console.Write("Podaj nazwe druzyny: ");
            string teamname = Console.ReadLine();
            if (teamname == "quit")
            {
                CloseEverything();
            }
            else
            {
                Homepage.SwitchSports(sportsname);
                Homepage.SwitchTabs(teamname);
            }
        }

        internal static void Getter(string teamname)
        {
            if (teamname == "quit")
            {
                CloseEverything();
            }
            else
            {
                Homepage.SwitchTabs(teamname);
            }
        }

        internal static void Refresh()
        {
            webDriver.SwitchTo().Window(homewindow);
            webDriver.Navigate().Refresh();
            Base.CloseAlert();

            IJavaScriptExecutor js = ((IJavaScriptExecutor)webDriver);
            js.ExecuteScript("window.scrollTo(0, 0)");
        }

        internal static void CloseAlert()
        {
            try
            {
                WebDriverWait r = new WebDriverWait(Driver.webDriver, TimeSpan.FromSeconds(5));
                r.Until(ExpectedConditions.AlertIsPresent());
                Driver.webDriver.SwitchTo().Alert().Accept();
            }
            catch
            {

            }
        }

        internal static void CloseCookies()
        {
            try
            {
                Actions act = new Actions(Driver.webDriver);
                IWebElement cookie = Base.IsVisible(Driver.webDriver, By.XPath("//*[@id='cookie-law-content']/div/span"));
                act.MoveToElement(cookie).Click().Perform();
            }
            catch
            { // cookies juz zaakceptowane 
            }
        }

        internal static void CloseEverything()
        {
            webDriver.Quit();
            Environment.Exit(1);
        }
    }
}
