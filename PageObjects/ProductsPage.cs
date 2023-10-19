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
    public class ProductsPage
        
    {
        IWebDriver driver;

        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        By cardTitle      = By.CssSelector(".card-title a");
        By addToCart      = By.CssSelector(".card-footer button");
        By cards          = By.TagName("app-card");
        By checkOutButton = By.PartialLinkText("Checkout");

        public void waitForCheckOutDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public IList<IWebElement> getCards()
        {
            return driver.FindElements(cards);
        }

        public By getCardTitle()
        {
            return cardTitle;
        }

        public By addToCartButton()
        {
            return addToCart;
        }
        public CheckoutPage checkOut()
        {
            driver.FindElement(checkOutButton).Click();
            return new CheckoutPage(driver);
        }
    }
}
