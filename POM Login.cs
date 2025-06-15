using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SemosProject
{
    public class POM_Login
    {
        //LOCATORS
        private readonly By usernameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By LoginButton = By.XPath("//input[@id='login-button']");
        private readonly By errorMessage = By.XPath("//h3[@data-test='error']");
        private readonly IWebDriver _driver;
        
        
        public POM_Login(IWebDriver driver)
        {
            _driver = driver;
        }
        public void EnterUsername(string username)
        {
            _driver.FindElement(usernameField).Clear();
            _driver.FindElement(usernameField).SendKeys(username);
        }
        public void EnterPassword(string password)
        {
            _driver.FindElement(passwordField).Clear();
            _driver.FindElement(passwordField).SendKeys(password);
        }
        public void Login()
        {
            _driver.FindElement(LoginButton).Click();
        }
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            Login();
        }
        public string GetErrorMessage()
        {
            return _driver.FindElement(errorMessage).Text;
        }

    }
}
