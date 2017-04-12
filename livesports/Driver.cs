using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace livesports
{
    public static class Driver
    {
        public static IWebDriver webDriver = new ChromeDriver();

        public static void Goto(string url)
        {
            webDriver.Url = url;
            WaitForLoaded(15); 
            webDriver.Manage().Window.Maximize();
        }
        public static void WaitForLoaded(int sec)
        {
            webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(sec));
            Base.CloseAlert();
        }
    }
}