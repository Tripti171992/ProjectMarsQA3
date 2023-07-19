using Mars1.Data;
using Mars1.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars1.Pages
{
    public class HomePage : CommonDriver
    {

        private IWebElement SignInButton => driver.FindElement(By.XPath("//*[text()='Sign In']"));
        private IWebElement EmailTextbox => driver.FindElement(By.XPath("//*[@placeholder='Email address']"));
        private IWebElement PasswordTextbox => driver.FindElement(By.XPath("//*[@placeholder='Password']"));
        private IWebElement LoginButton => driver.FindElement(By.XPath("//*[text()='Login']"));
        private IWebElement UserNameLable => driver.FindElement(By.XPath("//a[text()=' Chat']/following-sibling::span"));
        private IWebElement JoinButton => driver.FindElement(By.XPath("//button[text()='Join']"));
        private IWebElement FirstNameTextbox => driver.FindElement(By.XPath("//input[@placeholder='First name']"));
        private IWebElement LastNameTextbox => driver.FindElement(By.XPath("//input[@placeholder='Last name']"));
        private IWebElement ConfirmPasswordTextbox => driver.FindElement(By.XPath("//input[@placeholder='Confirm Password']"));
        private IWebElement Checkbox => driver.FindElement(By.XPath("//input[@type='checkbox']"));
        private IWebElement FinalJoinButton => driver.FindElement(By.XPath("//*[@id='submit-btn']"));
        private IWebElement RegisterationMessage => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        public void SignIn()
        {
            //------Signing in into Mars--------

            //Launch URL for Mars
            NavigateUrl();

            //Click on "Sign In" button
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Sign In']", 4);

            SignInButton.Click();

            //Enter the valid email address and password.
            EmailTextbox.SendKeys(UserInformation.EmailAddress);
            PasswordTextbox.SendKeys(UserInformation.Password);

            //Click on "Login" button
            LoginButton.Click();
        }
        public void Login(string email, string password)
        {
            //------Signing in into Mars--------

            //Launch URL for Mars
            NavigateUrl();

            //Click on "Sign In" button
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Sign In']", 4);

            SignInButton.Click();

            //Enter the invalid email address and password.
            EmailTextbox.SendKeys(email);
            PasswordTextbox.SendKeys(password);

            //Click on "Login" button
            LoginButton.Click();
        }
        public string GetUserName()
        {
            //Return username
            try
            {
                Wait.WaitToExist(driver, "XPath", "//a[text()=' Chat']/following-sibling::span", 3);
                return UserNameLable.Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void Registration()
        {
            //--------Registering into Mars--------

            NavigateUrl();

            //Click on "Join" button.
            Wait.WaitToBeClickable(driver, "XPath", "//button[text()='Join']", 4);
            JoinButton.Click();

            //Enter new user information
            FirstNameTextbox.SendKeys(UserInformation.FirstName);
            LastNameTextbox.SendKeys(UserInformation.LastName);
            EmailTextbox.SendKeys(UserInformation.EmailAddress);
            PasswordTextbox.SendKeys(UserInformation.Password);
            ConfirmPasswordTextbox.SendKeys(UserInformation.ConfirmPassword);
            Checkbox.Click();
            FinalJoinButton.Click();
        }
        public string GetRegistrationMessage()
        {
            //return registration message
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 8);
            return RegisterationMessage.Text;
        }
    }
}
