using Mars1.Data;
using Mars1.Pages;
using NUnit.Framework;
using System;
using System.Reflection.Emit;
using TechTalk.SpecFlow;

namespace Mars1.StepDefinitions
{
    [Binding]
    public class LanguageFeatureStepDefinitions
    {
        //Initializing Profile page
        ProfilePage ProfilePageObj;
        public LanguageFeatureStepDefinitions()
        {
            ProfilePageObj = new ProfilePage();
        }

        [When(@"I added language '([^']*)' and level '([^']*)'")]
        public void WhenIAddedLanguageAndLevel(string language, string level)
        {
            //Adding a language
            ProfilePageObj.AddLanguage(language, level);
        }

        [Then(@"A language '([^']*)' added success message should be displayed")]
        public void ThenALanguageAddedSuccessMessageShouldBeDisplayed(string language)
        {
            //Verifying success message for addition of a language record
            string expectedMessage = language + " has been added to your languages";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Language not added!!");
        }

        [Then(@"A language '([^']*)' and Level '([^']*)' record should be added successfully")]
        public void ThenALanguageAndLevelRecordShouldBeAddedSuccessfully(string language, string level)
        {
            //Verify the added language record in the table
            string newAddedLanguage = ProfilePageObj.GetLanguage();
            string newAddedLanguageLevel = ProfilePageObj.GetLanguageLevel();
            Assert.AreEqual(language, newAddedLanguage, "Actual and expected language do not match. Language not added!!");
            Assert.AreEqual(level, newAddedLanguageLevel, "Actual and expected language level do not match. Language level not added !!");
        }

        [When(@"I update existing language '([^']*)' and level '([^']*)' to new language '([^']*)' and level '([^']*)' of an existing record")]
        public void WhenIUpdateExistingLanguageAndLevelToNewLanguageAndLevelOfAnExistingRecord(string existingLanguage, string existingLevel, string language, string level)
        {
            //Updating a language
            ProfilePageObj.UpdateLanguage(existingLanguage, existingLevel, language, level);
        }

        [Then(@"A language '([^']*)' updated success message should be displayed")]
        public void ThenALanguageUpdatedSuccessMessageShouldBeDisplayed(string language)
        {
            //Verifying success message for updation of a language record
            string expectedMessage = language + " has been updated to your languages";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message match. Language not updated!!");
        }

        [Then(@"New language '([^']*)' and level '([^']*)' should get updated in existing language record successfully")]
        public void ThenNewLanguageAndLevelShouldGetUpdatedInExistingLanguageRecordSuccessfully(string language, string level)
        {
            //Verify language record updated
            string result = ProfilePageObj.GetUpdatedLanguageResult(language, level);
            Assert.AreEqual("Updated", result, "Actual and expected result do not match. Language not deleted!!");
        }

        [When(@"I delete a language '([^']*)' and a level '([^']*)'  record")]
        public void WhenIDeleteALanguageAndALevelRecord(string language, string level)
        {
            //Deleting a language
            ProfilePageObj.DeleteLanguage(language, level);
        }

        [Then(@"A language '([^']*)' deleted success message should be displayed")]
        public void ThenALanguageDeletedSuccessMessageShouldBeDisplayed(string language)
        {
            //Verifying success message for deletion of a language record
            string expectedMessage = language + " has been deleted from your languages";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(actualMessage, expectedMessage, "Actual and expected language match. Language deleted!!");
        }

        [Then(@"A language '([^']*)' and level '([^']*)'record should not exist")]
        public void ThenALanguageAndLevelRecordShouldNotExist(string language, string level)
        {
            //Verify the language record deleted
            string result = ProfilePageObj.GetDeleteLanguageResult(language, level);
            Assert.AreEqual("Deleted", result, "Actual and expected result do not match. Language not deleted!!");
        }

        [Then(@"A language not added error message should be displayed")]
        public void ThenALanguageNotAddedErrorMessageShouldBeDisplayed()
        {
            //Verifying error message for invalid language record addition
            string expectedMessage = "Please enter language and level";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Language record added!!");

        }

        [Then(@"A record with invalid language '([^']*)' or/and no level '([^']*)' should not be added")]
        public void ThenARecordWithInvalidLanguageOrAndNoLevelShouldNotBeAdded(string language, string level)
        {
            //Verify the invalid added language record not added in the table
            string newAddedLanguage = ProfilePageObj.GetLanguage();
            string newAddedLanguageLevel = ProfilePageObj.GetLanguageLevel();
            Assert.AreNotEqual(language, newAddedLanguage, "Actual and expected language match");
            Assert.AreNotEqual(level, newAddedLanguageLevel, "Actual and expected language level do not match!!");
        }

        [Then(@"A language not updated error message should be displayed")]
        public void ThenALanguageNotUpdatedErrorMessageShouldBeDisplayed()
        {
            //Verifying error message for invalid language updation in a record
            string expectedMessage = "Please enter language and level";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.Language not updated!!");
        }

        [Then(@"Invalid language '([^']*)' and level '([^']*)' should not get updated in existing language record")]
        public void ThenInvalidLanguageAndLevelShouldNotGetUpdatedInExistingLanguageRecord(string language, string level)
        {
            //Verify language record not updated
            string result = ProfilePageObj.GetUpdatedLanguageResult(language, level);
            Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Language not deleted!!");
        }

        [Then(@"A duplicate language added error message should be displayed")]
        public void ThenADuplicateLanguageAddedErrorMessageShouldBeDisplayed()
        {
            //Verifying an error message for duplicate language updation displayed
            string expectedMessage = "This language is already exist in your language list.";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.Duplicate language record added!!");
        }

        [Then(@"A duplicate language '([^']*)' and Level record should not be added successfully")]
        public void ThenADuplicateLanguageAndLevelRecordShouldNotBeAddedSuccessfully(string language)
        {
            int dublicateRecordCount = ProfilePageObj.DuplicateRecordCount(language);
            Assert.AreEqual(1, dublicateRecordCount, "Actual and expected value do not match. Duplicate language record added!!");
        }

        [When(@"I cancelled adding a language '([^']*)' and level '([^']*)' record")]
        public void WhenICancelledAddingALanguageAndLevelRecord(string language, string level)
        {
            //Cancel adding a language
            ProfilePageObj.CancelLanguageAddition(language, level);
        }

        [Then(@"A language '([^']*)' record addition should be cancelled")]
        public void ThenALanguageRecordAdditionShouldBeCancelled(string language)
        {
            //Verify the language record not added on cancelling
            string newLanguage = ProfilePageObj.GetLanguage();
            Assert.AreNotEqual(language, newLanguage, "Actual and expected language match. Language  added !!");
        }

        [When(@"I cancelled updating existing language '([^']*)' and level '([^']*)' to new language '([^']*)' and level '([^']*)' of an existing record")]
        public void WhenICancelledUpdatingExistingLanguageAndLevelToNewLanguageAndLevelOfAnExistingRecord(string existingLanguage, string existingLevel, string language, string level)
        {
            //Updating a language
            ProfilePageObj.CancelLanguageUpdation(existingLanguage, existingLevel, language, level);
        }

        [Then(@"A language '([^']*)' and level '([^']*)' record updation should be cancelled")]
        public void ThenALanguageAndLevelRecordUpdationShouldBeCancelled(string language, string level)
        {
            //Verify language record not updated
            string result = ProfilePageObj.GetUpdatedLanguageResult(language, level);
            Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Language not deleted!!");
        }

    }
}
