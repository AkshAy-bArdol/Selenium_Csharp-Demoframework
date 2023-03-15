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

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement location;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement country;

        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement checkBox;

        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement purchase;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMsg;

        public void sendcountryInitials()
        {
            location.SendKeys("Ind");
        }
        public void waitForDisplay()
        {
             wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public void selectCountry()
        {
            country.Click();
        }

        public void selectCheckbox()
        {
            checkBox.Click();
        }

        public void clickOnPurchase()
        {
            purchase.Click();
        }

        public String getSuccessMsg()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".alert-success")));
            return successMsg.Text;
        }




    }
}
