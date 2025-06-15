using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.WebRequestMethods;


namespace SemosProject
{
    public class Tests
    {
        private IWebDriver _driver;
        LoginPage login;
        private string url = "https://www.saucedemo.com/";

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl( url );
            _driver.Manage().Window.Maximize();
            login = new LoginPage(_driver);

        }

        [Test]
        public void TestLoginSuccess()

        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(2000);

            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }
        }

        [Test]
        public void TestLoginSuccess1()

        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("problem_user", "secret_sauce");
            Thread.Sleep(2000);

            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }
        }

        [Test]
        public void TestLoginSuccess2()

        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("locked_out_user", "secret_sauce");
            Thread.Sleep(2000);

            if (_driver.Url != "https://www.saucedemo.com/inventory.html")      /*----> BUG!!*/
            {
                throw new Exception("Login failed or incorrect redirect.");
            }
        }

        [Test]
        public void TestLoginSuccess3()

        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("error_user", "secret_sauce");
            Thread.Sleep(2000);

            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }
        }
        [Test]
        public void TestLoginWrongUsername()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("Thunder_user", "secret_sauce");
            Thread.Sleep(2000);

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"), "Error message not as expected.");

        }
        [Test]
        public void TestLoginWrongPass()
        {
            POM_Login loginPage = new POM_Login( _driver);
            loginPage.Login("problem_user", "Sauce_secret");
            Thread.Sleep(2000);

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username and password do not match any user in this service"), "Error message not as expected.");

        }

        [Test]
        public void TestLoginBlankPass()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("", "secret_sauce");
            Thread.Sleep(2000);

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Username is required"), "Error message not as expected.");
        }

        [Test]
        public void TestLoginBlankUsername()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user","");
            Thread.Sleep(2000);

            string errorMessage = loginPage.GetErrorMessage();
            Assert.That(errorMessage, Is.EqualTo("Epic sadface: Password is required"), "Error message not as expected.");

        }
        [Test]
        public void TestAddProductToCart()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(2000);
            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }

            HomePage_POM homepage = new HomePage_POM(_driver);
            homepage.AddProductToCart("Sauce Labs Backpack");
            Thread.Sleep(2000);

            Assert.That(homepage.CartBadge, Is.EqualTo("1"), "Number of items incorrect!");
            Thread.Sleep(2000);
        }
        [Test]
        public void TestAddProductToCart1()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(2000);
            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }

            HomePage_POM homepage = new HomePage_POM(_driver);
            homepage.AddProductToCart("Sauce Labs Backpack");
            Thread.Sleep(2000);
            homepage.AddProductToCart("Sauce Labs Bike Light");
            Thread.Sleep(2000);
            homepage.AddProductToCart("Sauce Labs Onesie");
            Thread.Sleep(2000);

            Assert.That(homepage.CartBadge, Is.EqualTo("3"), "Number of items incorrect!");
            Thread.Sleep(2000);
        }
        [Test]
        public void TestRemoveProducts()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(2000);
            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }

            HomePage_POM homepage = new HomePage_POM(_driver);
            homepage.AddProductToCart("Sauce Labs Bike Light");
            Thread.Sleep(2000);
            homepage.AddProductToCart("Sauce Labs Backpack");
            Thread.Sleep(2000);

            Assert.That(homepage.CartBadge, Is.EqualTo("2"), "Number of items incorrect!");
            Thread.Sleep(1000);

            homepage.RemoveProductFromCart("Sauce Labs Bike Light");
            Thread.Sleep(2000);
            homepage.RemoveProductFromCart("Sauce Labs Backpack");
            Thread.Sleep(2000);

            Assert.That(homepage.CartBadge, Is.EqualTo(""), "Number of items incorrect!");
            Thread.Sleep(2000);
        }

        [Test]
        public void TestLogout2()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(1000);
            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }

            HomePage_POM homepage = new HomePage_POM(_driver);
            homepage.AddProductToCart("Sauce Labs Bike Light");
            Thread.Sleep(2000);
            homepage.AddProductToCart("Sauce Labs Backpack");
            Thread.Sleep(2000);
            Assert.That(homepage.CartBadge, Is.EqualTo("2"), "Number of items incorrect!");
            Thread.Sleep(1000);

            homepage.RemoveProductFromCart("Sauce Labs Bike Light");
            Thread.Sleep(2000);
            homepage.RemoveProductFromCart("Sauce Labs Backpack");
            Thread.Sleep(2000);

            Assert.That(homepage.CartBadge, Is.EqualTo(""), "Number of items incorrect!");
            Thread.Sleep(2000);

            HomePage_POM homePage_POM = new HomePage_POM(_driver);
            homepage.Logout("HamburgerMenu");
            Thread.Sleep(1000);
            homepage.Logout1("LogoutLink");



        }
        [Test]
        public void TestLogout()
        {
            POM_Login loginPage = new POM_Login(_driver);
            loginPage.Login("standard_user", "secret_sauce");
            Thread.Sleep(1000);
            if (_driver.Url != "https://www.saucedemo.com/inventory.html")
            {
                throw new Exception("Login failed or incorrect redirect.");
            }

            HomePage_POM homepage = new HomePage_POM(_driver);
            homepage.Logout("HamburgerMenu");
            Thread.Sleep(1000);
            homepage.Logout1("LogoutLink");
        }

        [TearDown]

        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }  
}   