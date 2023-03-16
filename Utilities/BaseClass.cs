using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpDemoFramework.Utilities
{
    public class BaseClass
    {
        //public IWebDriver driver;
        public ExtentReports extent;
        public ExtentTest test;
        String reportPath;
        String browserName;
        //reports
        [OneTimeSetUp]
        public void SetUp()
        {
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
             reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local Host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Akshay");
        }

        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //Configuration
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }
        public void InitBrowser(String browserName)
        {
            switch (browserName)
            {
                case "FireFox":
                    driver.Value = new FirefoxDriver();
                    break;

                case "Chrome":
                    driver.Value = new ChromeDriver();
                    break;

                case "Edge":
                    driver.Value = new EdgeDriver();
                    break;

            }
        }
        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
        [OneTimeTearDown]
        public void SendEmail()
        {
            // Create the email message
            MailMessage message = new MailMessage();
            message.To.Add("bardolakshay@gmail.com");
            message.From = new MailAddress("akshay.b@simformsolutions.com"); // Set the "From" address
            message.Subject = "Test Automation Report";
            message.Body = "Please find attached the latest test report.";

            // Attach the Extent Report to the email message
            Attachment attachment = new Attachment(reportPath);
            message.Attachments.Add(attachment);

            // Set the email credentials and SMTP server details
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("akshay.b@simformsolutions.com", "s8o?Utubr");
            client.EnableSsl = true;

            // Send the email
            client.Send(message);
        }

    }
}


