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

        //pageobject factory //div[@class='form-group'][5]/label/span/input

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userName;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkBox;

        [FindsBy(How = How.CssSelector, Using = "#signInBtn")]
        private IWebElement signInButton;

        public ProductsPage validLogin(String user,String pass)
        {
            userName.SendKeys(user);
            password.SendKeys(pass);
            checkBox.Click();
            signInButton.Click();
            return new ProductsPage(driver);
        }

    }
}
