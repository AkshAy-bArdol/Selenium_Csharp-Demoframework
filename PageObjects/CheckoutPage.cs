using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemoFramework.PageObjects
{
    public class CheckoutPage
    {
        IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        By checkoutCards = By.CssSelector("h4 a");
        By checkOutButton = By.CssSelector(".btn-success");

        public IList<IWebElement> getCards()
        {
            return driver.FindElements(checkoutCards);
        }

        public SuccessPage checkOut()
        {
            driver.FindElement(checkOutButton).Click();
            return new SuccessPage(driver);
        }
    }
}
