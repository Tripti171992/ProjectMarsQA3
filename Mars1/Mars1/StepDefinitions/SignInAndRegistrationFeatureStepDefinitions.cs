using Mars1.Data;
using Mars1.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace Mars1.StepDefinitions
{
    [Binding]
    public class SignInAndRegistrationFeatureStepDefinitions
    {//Initializing Home page
        HomePage HomePageObj;
        public SignInAndRegistrationFeatureStepDefinitions()
        {
            HomePageObj = new HomePage();
        }

        [Given(@"I registered into the Mars portal")]
        public void GivenIRegisteredIntoTheMarsPortal()
        {
            //Registering to Mars
            HomePageObj.Registration();
        }

        [Then(@"A user should be registered successfully")]
        public void ThenAUserShouldBeRegisteredSuccessfully()
        {
            string message = HomePageObj.GetRegistrationMessage();
            Assert.AreEqual("Registration successful", message, "Actual and expected message do not match.User not registered in successfully !!");
        }

        [Given(@"I logged into the Mars portal successfully")]
        public void GivenILoggedIntoTheMarsPortalSuccessfully()
        {
            //Signing in into Mars
            HomePageObj.SignIn();
        }

        [Then(@"A user should be taken to home page successfully")]
        public void ThenAUserShouldBeTakenToHomePageSuccessfully()
        {
            //Verify if user is taken to their home page upon login in to Mars 
            string expectedUserName = "Hi " + UserInformation.FirstName;
            string actualUserName = HomePageObj.GetUserName();
            Assert.AreEqual(expectedUserName, actualUserName, "Actual and expected username do not match.User not logged in successfully !!");
        }

        [Given(@"I logged into the Mars portal with invalid credentials '([^']*)''([^']*)'")]
        public void GivenILoggedIntoTheMarsPortalWithInvalidCredentials(string email, string password)
        {
            //Signing in into Mars with invalid credentials
            HomePageObj.Login(email, password);
        }

        [Then(@"A user should not be taken to home page successfully")]
        public void ThenAUserShouldNotBeTakenToHomePageSuccessfully()
        {

            string actualUserName = HomePageObj.GetUserName();
            bool result;

            if (actualUserName.StartsWith("Hi "))
            {
                result = false;
            }
            else
            {
                result = true;
            }
            Assert.IsTrue(result, "Logged in!! ");
        }
    }
}
