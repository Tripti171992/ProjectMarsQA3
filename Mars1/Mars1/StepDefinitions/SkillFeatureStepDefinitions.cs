using Mars1.Data;
using Mars1.Pages;
using NUnit.Framework;
using System;
using System.Reflection.Emit;
using TechTalk.SpecFlow;

namespace Mars1.StepDefinitions
{
    [Binding]
    public class SkillFeatureStepDefinitions
    {
        //Initializing Profile page
        ProfilePage ProfilePageObj;
        public SkillFeatureStepDefinitions()
        {
            ProfilePageObj = new ProfilePage();
        }

        [When(@"I added '([^']*)' and '([^']*)'")]
        public void WhenIAddedAnd(string skill, string skillLevel)
        {
            //Adding a skill
            ProfilePageObj.AddSkill(skill, skillLevel);
        }

        [Then(@"A skill '([^']*)' added success message should be displayed")]
        public void ThenASkillAddedSuccessMessageShouldBeDisplayed(string skill)
        {
            //Verifying success message for addition of a skill record
            string expectedMessage = skill + " has been added to your skills";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Skill not added!!");
        }

        [Then(@"A '([^']*)' and '([^']*)' record should be added successfully")]
        public void ThenAAndRecordShouldBeAddedSuccessfully(string skill, string skillLevel)
        {
            //Verify the new skill record added in the table.
            string newAddedSkill = ProfilePageObj.GetSkill();
            string newAddedSkillLevel = ProfilePageObj.GetSkillLevel();
            Assert.AreEqual(skill, newAddedSkill, "Actual and expected skill do not match. Skill not added !!");
            Assert.AreEqual(skillLevel, newAddedSkillLevel, "Actual and expected skill level do not match. Skill Level not added !!");
        }

        [When(@"I update existing skill '([^']*)' and level '([^']*)' to new skill '([^']*)' and level '([^']*)' of an existing record")]
        public void WhenIUpdateExistingSkillAndLevelToNewSkillAndLevelOfAnExistingRecord(string existingSkill, string existingLevel, string skill, string skillLevel)
        {
            //updating a skill
            ProfilePageObj.UpdateSkill(existingSkill, existingLevel, skill, skillLevel);
        }

        [Then(@"A skill '([^']*)' updated success message should be displayed")]
        public void ThenASkillUpdatedSuccessMessageShouldBeDisplayed(string skill)
        {
            //Verifying success message for updation of a skill record
            string expectedMessage = skill + " has been updated to your skills";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Skill not updated!!");
        }

        [Then(@"New skill '([^']*)' and level '([^']*)' should get updated in existing skill record successfully")]
        public void ThenNewSkillAndLevelShouldGetUpdatedInExistingSkillRecordSuccessfully(string skill, string level)
        {
            //Verify skill record updated
            string result = ProfilePageObj.GetUpdatedSkillResult(skill, level);
            Assert.AreEqual("Updated", result, "Actual and expected result do not match. Skill not deleted!!");
        }

        [When(@"I delete a skill '([^']*)' and a level '([^']*)'  record")]
        public void WhenIDeleteASkillAndALevelRecord(string skill, string level)
        {
            //Deleting a skill
            ProfilePageObj.DeleteSkill(skill, level);
        }

        [Then(@"A skill '([^']*)' deleted success message should be displayed")]
        public void ThenASkillDeletedSuccessMessageShouldBeDisplayed(string skill)
        {
            //Verifying success message for deletion of a skill record
            string expectedMessage = skill + " has been deleted";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(actualMessage, expectedMessage, "Actual and expected skill match. Skill deleted!!");
        }

        [Then(@"A skill '([^']*)' and level '([^']*)'record should not exist")]
        public void ThenASkillAndLevelRecordShouldNotExist(string skill, string level)
        {
            //Verify the skill record deleted
            string result = ProfilePageObj.GetDeleteLanguageResult(skill, level);
            Assert.AreEqual("Deleted", result, "Actual and expected result do not match. Skill not deleted!!");
        }

        [Then(@"A skill not added success message should be displayed")]
        public void ThenASkillNotAddedSuccessMessageShouldBeDisplayed()
        {
            //Verifying error message for addition of invalid skill record
            string expectedMessage = "Please enter skill and experience level";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Skill record added!!");
        }

        [Then(@"A record with invalid skill '([^']*)' or/and no level '([^']*)' should not be added")]
        public void ThenARecordWithInvalidSkillOrAndNoLevelShouldNotBeAdded(string skill, string skillLevel)
        {
            //Verify the invalid skill record not added in the table.
            string newAddedSkill = ProfilePageObj.GetSkill();
            string newAddedSkillLevel = ProfilePageObj.GetSkillLevel();
            Assert.AreNotEqual(skill, newAddedSkill, "Actual and expected skill match!!");
            Assert.AreNotEqual(skillLevel, newAddedSkillLevel, "Actual and expected skill level match!!");
        }

        [Then(@"A skill not updated error message should be displayed")]
        public void ThenASkillNotUpdatedErrorMessageShouldBeDisplayed()
        {
            //Verifying error message for invalid skill updation in a record
            string expectedMessage = "Please enter skill and experience level";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match.Skill not updated!!");
        }

        [Then(@"A record with empty skill '([^']*)' and level '([^']*)' should not get updated in existing language record")]
        public void ThenARecordWithEmptySkillAndLevelShouldNotGetUpdatedInExistingLanguageRecord(string skill, string level)
        {
            //Verify invalid skill record not updated
            string result = ProfilePageObj.GetUpdatedSkillResult(skill, level);
            Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Language not deleted!!");
        }

        [Then(@"A duplicate skill added error message should be displayed")]
        public void ThenADuplicateSkillAddedErrorMessageShouldBeDisplayed()
        {
            //Verifying an error message for duplicate skill updation displayed
            string expectedMessage = "This skill is already exist in your skill list.";
            string actualMessage = ProfilePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match. Duplicat skill record added!!");
        }

        [Then(@"A duplicate skill '([^']*)' and Level record should not be added successfully")]
        public void ThenADuplicateSkillAndLevelRecordShouldNotBeAddedSuccessfully(string skill)
        {
            int dublicateRecordCount = ProfilePageObj.DuplicateRecordCount(skill);
            Assert.AreEqual(1, dublicateRecordCount, "Actual and expected value do not match. Duplicate skill record added!!");
        }

        [When(@"I cancelled adding a skill '([^']*)' and  level '([^']*)' reord")]
        public void WhenICancelledAddingASkillAndLevelReord(string skill, string skillLevel)
        {
            //Cancel Adding Skill
            ProfilePageObj.CancelSkillAddition(skill, skillLevel);
        }

        [Then(@"A skill '([^']*)' record addition should be cancelled")]
        public void ThenASkillRecordAdditionShouldBeCancelled(string skill)
        {
            //Verify the skill record not added on cancelling.
            string newSkill = ProfilePageObj.GetSkill();
            Assert.AreNotEqual(skill, newSkill, "Actual and expected skill match. Skill  added !!");
        }

        [When(@"I cancelled updating existing skill '([^']*)' and level '([^']*)' to new skill '([^']*)' and level '([^']*)' of an existing record")]
        public void WhenICancelledUpdatingExistingSkillAndLevelToNewSkillAndLevelOfAnExistingRecord(string existingSkill, string existingLevel, string skill, string skillLevel)
        {
            //updating a skill
            ProfilePageObj.CancelSkillUpdation(existingSkill, existingLevel, skill, skillLevel);
        }

        [Then(@"A skill '([^']*)' and level '([^']*)' record updation should be cancelled")]
        public void ThenASkillAndLevelRecordUpdationShouldBeCancelled(string skill, string level)
        {
            //Verify skill record not updated
            string result = ProfilePageObj.GetUpdatedSkillResult(skill, level);
            Assert.AreEqual("Not updated", result, "Actual and expected result do not match. Skill not deleted!!");
        }

    }
}
