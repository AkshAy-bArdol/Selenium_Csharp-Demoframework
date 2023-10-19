using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemoFramework.PageObjects
{
    public class SuccessPage
    {
        IWebDriver driver;
        WebDriverWait wait;
        public SuccessPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        By location   = By.Id("country");
        By country    = By.LinkText("India");
        By checkBox   = By.XPath("//label[@for='checkbox2']");
        By purchase   = By.XPath("//input[@type='submit']");
        By successMsg = By.CssSelector(".alert-success");

        public void sendcountryInitials()
        {
            driver.FindElement(location).SendKeys("Ind");
        }
        public void waitForDisplay()
        {
             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public void selectCountry()
        {
            driver.FindElement(country).Click();
        }

        public void selectCheckbox()
        {
            driver.FindElement(checkBox).Click();
        }

        public void clickOnPurchase()
        {
            driver.FindElement(purchase).Click();
        }

        public String getSuccessMsg()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));
            return driver.FindElement(successMsg).Text;
        }




    }
}
