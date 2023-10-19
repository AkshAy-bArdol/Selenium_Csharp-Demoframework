using OpenQA.Selenium;
using SeleniumCsharpDemoFramework.PageObjects;
using SeleniumCsharpDemoFramework.Utilities;

namespace SeleniumCsharpDemoFramework.Tests
{
    [Parallelizable(ParallelScope.Children)]
    public class Tests : BaseClass
    {
        [Test,Category("Regression")]
        /*[TestCase("rahulshettyacademy", "learning")]
        [TestCase("rahulshetty", "learning")]*/
        [TestCaseSource("AddTestDataConfig")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String userName,String Password, String[] expectedProducts)
        {
            String[] actualProducts = new string[2];
            LoginPage loginpage = new LoginPage(getDriver());
            ProductsPage productspage =loginpage.validLogin(userName, Password);
            productspage.waitForCheckOutDisplay();

            IList<IWebElement> products= productspage.getCards();
            foreach(IWebElement product in products)
            {
                if(expectedProducts.Contains(product.FindElement(productspage.getCardTitle()).Text))
                {
                    product.FindElement(productspage.addToCartButton()).Click();
                }   
            }

            CheckoutPage checkoutPage = productspage.checkOut();
            IList<IWebElement> checkoutCards= checkoutPage.getCards();
            for(int i=0;i<checkoutCards.Count;i++) 
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            SuccessPage successPage=checkoutPage.checkOut();
            successPage.sendcountryInitials();
            successPage.selectCountry();
            successPage.selectCheckbox();
            successPage.clickOnPurchase();
            String confirmMsg = successPage.getSuccessMsg();
            
            StringAssert.Contains("Success", confirmMsg);

        }

        [Test,Category("Smoke")]

        public void LocatorsIdentificataions()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Id("username")).Clear();
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.Value.FindElement(By.Name("password")).SendKeys("1235456");
            driver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();
            Thread.Sleep(3000);
            String actualerrorMessage = driver.Value.FindElement(By.CssSelector(".form div strong")).Text;
            TestContext.Progress.WriteLine(actualerrorMessage);
            TestContext.Progress.WriteLine(actualerrorMessage);
            Assert.That(actualerrorMessage, Is.EqualTo("Incorrect"));

        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("userName"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("userName"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("userName_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
} 
