using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SemosProject
{
    internal class Shopping_cart
    {
        private readonly IWebDriver _driver;

        //locators
        private readonly By CheckoutButton = By.Id("checkout");
        private readonly By FirstName = By.XPath("//input[@id='first-name']");
        private readonly By LastName = By.XPath("//input[@id='last-name']");
        private readonly By PostalCode = By.Id("postal-code");


        public Shopping_cart(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Firstname(string name)
        {
            _driver.FindElement(FirstName).Clear();
            _driver.FindElement(FirstName).SendKeys(name);
        }
        public void Lastname(string lastname)
        {
            _driver.FindElement(LastName).Clear();
            _driver.FindElement(LastName).SendKeys(lastname);
        }
        public void Checkout()
        {
            _driver.FindElement(CheckoutButton).Click();
        }
        public void Checkout(string name, string lastname)
        {
            Firstname(name);
            Lastname(lastname);
            Checkout();
        }
    }
 }

  
  
