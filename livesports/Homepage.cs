using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace livesports
{
    public static class Homepage
    {
        private static string homewindow;
        private static IWebDriver webDriver = Driver.webDriver;
        public static bool Found { get; private set; }

        public static void SwitchSports(string sport)
        {
            bool found_sport=false;
            IList<IWebElement> rows = Base.AreVisible(webDriver, By.CssSelector("#menu ul li"));
            try
            {
                Actions act = new Actions(webDriver);
                for (int i = 0; i <= 7; i++)
                {
                    if (rows[i].Text.Contains(sport.ToUpper()))
                    {
                        act.MoveToElement(rows[i]).Click().Build().Perform();
                        found_sport = true;
                    }
                }
                if (!found_sport)
                {
                    IWebElement moresports = Base.IsVisible(webDriver, By.CssSelector("#menu ul li.minority"));
                    act.MoveToElement(moresports).Click().Build().Perform();
                    IList<IWebElement> more = moresports.FindElements(By.CssSelector("ul#menumin li"));
                    for (int i = 0; i <= more.Count; i++)
                    {
                        if (rows[i].Text.Contains(sport.ToUpper()))
                        {
                            act.MoveToElement(rows[i]).Click().Build().Perform();
                            found_sport = true;
                        }
                    }
                }
                if (!found_sport)
                {
                    Console.WriteLine("Nie znaleziono dyscypliny.");
                    Base.Getter();
                }
            }
            catch { }
        }
        

        public static void SwitchTabs(string teamname)
        {
            Found = false;
            Base.homewindow = webDriver.CurrentWindowHandle;

            Console.Clear();
            Console.WriteLine("Trwa wyszukiwanie druzyny: " + teamname);

            Actions act = new Actions(webDriver);
            string[] tabs = { "li0", "li1", "li2" };

            for (int i = 0; i <= 2; i++)
            {
                if (!Found)
                {
                    IWebElement tab_all = Base.IsVisible(webDriver, By.CssSelector("div#fscon ul li." + tabs[i]));
                    act.MoveToElement(tab_all).Click().Build().Perform();
                    FindInTabSwitch(teamname);
                }
            }
            if (!Found)
            {
                Console.WriteLine("Nie znaleziono druzyny.");
                Base.Getter();
            }
        }
        private static void FindInTabSwitch(string teamname)
        {
            Found = false;
            homewindow = webDriver.CurrentWindowHandle;
            Actions act = new Actions(webDriver);
            IList<IWebElement> team = Base.AreVisible(webDriver, By.CssSelector("div#fs div table tbody tr"));
            if (team != null)
            {
                foreach (IWebElement t in team)
                {
                    if (t.Text.Contains(teamname))
                    {
                        act.MoveToElement(t).Click().Build().Perform();
                        Found = true;
                        break;
                    }
                }
            }
            if (Found)
            {
                Details.GetDetails();
            }
            else
            {
            }
        }
    }
}
