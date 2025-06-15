using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SemosProject
{

    public class HomePage_POM
    {
        private readonly IWebDriver _driver;

        // Locators
        private readonly By ProductName = By.ClassName("inventory_item_name");
        private readonly By ShoppingCartBadge = By.ClassName("shopping_cart_badge");
        private readonly By HamburgerMenu = By.Id("react-burger-menu-btn");
        private readonly By LogoutLink = By.XPath("//a[@id='logout_sidebar_link']");

        public HomePage_POM(IWebDriver driver)
        {
           _driver = driver; 
        }

        public List<string> GetProductName()
        {
            return _driver.FindElements(ProductName).Select(e => e.Text).ToList();
        }

        public void AddProductToCart(string productName)
        {
            string xpath = $"//div[@class='inventory_item_name ' and text()='{productName}']/ancestor::div[@class='inventory_item']//button[contains(text(), 'Add to cart')]";
            _driver.FindElement(By.XPath(xpath)).Click();
        }
        public void RemoveProductFromCart(string productName)
        {
            string xpath = $"//div[@class='inventory_item_name ' and text()='{productName}']/ancestor::div[@class='inventory_item']//button[contains(text(), 'Remove')]";
            _driver.FindElement(By.XPath(xpath)).Click();
        }
        public string CartBadge
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.ClassName("shopping_cart_badge")).Text;
                }
                catch (NoSuchElementException)
                {
                    return ""; // Badge not present = empty cart
                }
            }
        }

        public void Logout(string Hamburger)
        {
            _driver.FindElement(HamburgerMenu).Click();
            
            

        }
        public void Logout1(string Logout)
        {
     
            _driver.FindElement(LogoutLink).Click();
        }
        public bool IsLoggedOut()
        {
            return _driver.FindElement(By.Id("login-button")).Displayed;
        }
    }
}
