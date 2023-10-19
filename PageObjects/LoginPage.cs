using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpDemoFramework.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        By userName     = By.Id("username");
        By password     = By.Id("password");
        By checkBox     = By.XPath("//div[@class='form-group'][5]/label/span/input");
        By signInButton = By.CssSelector("#signInBtn");

        public ProductsPage validLogin(String user,String pass)
        {
            driver.FindElement(userName).SendKeys(user);
            driver.FindElement(password).SendKeys(pass);
            driver.FindElement(checkBox).Click();
            driver.FindElement(signInButton).Click();
            return new ProductsPage(driver);
        }

    }
}
