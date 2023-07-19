using Mars1.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection.Emit;
using Mars1.Data;
using TechTalk.SpecFlow.CommonModels;
using Gherkin.CucumberMessages.Types;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Mars1.Pages
{
    public class ProfilePage : CommonDriver
    {

        private IWebElement SkillTab => driver.FindElement(By.XPath("//a[text()='Skills']"));
        private IWebElement AddNewSkillButton => driver.FindElement(By.XPath("//*[text()='Skill']/following-sibling::th[2]/div"));
        private IWebElement SkillTextBox => driver.FindElement(By.XPath("//input[@placeholder='Add Skill']"));
        private IWebElement SkillLevelDropDown => driver.FindElement(By.XPath("//select[@class='ui fluid dropdown']"));
        private IWebElement AddButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private IWebElement NewSkillAddedCell => driver.FindElement(By.XPath("(//div[text()='Do you have any skills?']/parent::div/following-sibling::div//table/tbody)[last()]//td[1]"));
        private IWebElement NewSkillLevelAddedCell => driver.FindElement(By.XPath("(//div[text()='Do you have any skills?']/parent::div/following-sibling::div//table/tbody)[last()]//td[2]"));
        private IWebElement UpdateSkillIconButton => driver.FindElement(By.XPath("(//div[text()='Do you have any skills?']/parent::div/following-sibling::div//table/tbody)[last()]//td[3]/descendant::i[1]"));
        private IWebElement UpdateButton => driver.FindElement(By.XPath("//*[@value='Update']"));
        private IWebElement UpdatedSkillCell => driver.FindElement(By.XPath("(//div[text()='Do you have any skills?']/parent::div/following-sibling::div//table/tbody)[last()]//td[1]"));
        private IWebElement UpdatedSkillLevelCell => driver.FindElement(By.XPath("(//div[text()='Do you have any skills?']/parent::div/following-sibling::div//table/tbody)[last()]//td[2]"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//*[@value='Cancel']"));
        private IWebElement AddNewLanguageButton => driver.FindElement(By.XPath("//*[text()='Language']/following-sibling::th[2]/div"));
        private IWebElement LanguageTextBox => driver.FindElement(By.XPath("//*[@placeholder='Add Language']"));
        private IWebElement LanguageLevelDropDown => driver.FindElement(By.XPath("//select[@class='ui dropdown']"));
        private IWebElement NewLanguageAddedCell => driver.FindElement(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[1]"));
        private IWebElement NewLanguageLevelAddedCell => driver.FindElement(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[2]"));
        private IWebElement UpdateLanguageIconButton => driver.FindElement(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/tr/td[3]/span[1]/i"));
        private IWebElement UpdatedLanguageCell => driver.FindElement(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[1]"));
        private IWebElement UpdatedLanguageLevelCell => driver.FindElement(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[2]"));
        private IWebElement MessageWindow => driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
        private IWebElement CloseMessageIcon => driver.FindElement(By.XPath("//*[@class='ns-close']"));
        private string Message = "";
        public void AddLanguage(string language, string languageLevel)
        {
            //-----------Adding a language------------

            try
            {
                //If any message visible close it
                Wait.WaitToBeClickable(driver, "XPath", "//*[@class='ns-close']", 5);
                CloseMessageIcon.Click();
            }
            catch (Exception ex)
            {
                var exception = ex;
            }

            //Wait.WaitToBeClickable(driver, "XPath", AddNewLanguageButton.GetAttribute("XPath"), 5);
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Language']/following-sibling::th[2]/div", 5);

            //Click on "Add New" button            
            AddNewLanguageButton.Click();

            //Enter language and select language level
            LanguageTextBox.SendKeys(language);

            SelectElement languageLevelOption = new SelectElement(LanguageLevelDropDown);
            languageLevelOption.SelectByText(languageLevel);

            //Click on "Add" button.
            AddButton.Click();

            //Saving error or success message
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
            Message = MessageWindow.Text;
        }
        public string GetLanguage()
        {
            //Return new added langauge
            Wait.WaitToBeClickable(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[1]", 5);
            return NewLanguageAddedCell.Text;
        }
        public string GetLanguageLevel()
        {
            //Return new added langauge level
            Wait.WaitToBeClickable(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]/descendant::td[2]", 5);
            return NewLanguageLevelAddedCell.Text;
        }
        public string GetMessage()
        {
            //Returning error or success message
            return Message;
        }
        public void UpdateLanguage(string existingLanguage, string existingLevel, string language, string Level)
        {

            //---------updating a Language-----

            Wait.WaitToExist(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]", 6);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                // Get the text of the language and level column in the row
                IWebElement languageElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement languageLevel = row.FindElement(By.XPath("./td[2]"));
                string languageText = languageElement.Text;
                string languageLevelText = languageLevel.Text;

                // Check if the language matches the provided text
                if (languageText.Equals(existingLanguage, StringComparison.OrdinalIgnoreCase) && languageLevelText.Equals(existingLevel, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on update icon button of desired row

                    IWebElement languageUpdateIcon = row.FindElement(By.XPath("./td[3]/span[1]/i"));
                    languageUpdateIcon.Click();

                    //Clear textbox
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Add Language']", 5);
                    LanguageTextBox.SendKeys(Keys.Control + "A");
                    LanguageTextBox.SendKeys(Keys.Backspace);

                    //Enter laguage and language level
                    LanguageTextBox.SendKeys(language);

                    SelectElement languageLevelOption = new SelectElement(LanguageLevelDropDown);
                    languageLevelOption.SelectByText(Level);

                    //Click on "Update" button.            
                    UpdateButton.Click();

                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 5);
                    Message = MessageWindow.Text;

                    if (GetMessage() == "Please enter language and level")
                    {
                        CancelButton.Click();
                    }
                    break;
                }
            }
        }
        public string GetUpdatedLanguageResult(string language, string level)
        {
            //Return language updated result

            Wait.WaitToExist(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]", 6);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "";

            foreach (IWebElement row in rows)
            {
                // Get the text of the language and level column in the row
                IWebElement languageElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement languageLevel = row.FindElement(By.XPath("./td[2]"));
                string languageText = languageElement.Text;
                string languageLevelText = languageLevel.Text;

                // Check if the language matches the provided text
                if (languageText.Equals(language, StringComparison.OrdinalIgnoreCase) && languageLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    result = "Updated";
                    break;
                }
                else
                {
                    result = "Not updated";
                }
            }
            return result;
        }

        public void DeleteLanguage(string language, string level)
        {
            //---Deleting a language-----
            Wait.WaitToExist(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]", 6);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody/tr"));
            foreach (IWebElement row in rows)
            {
                // Get the text of the language and level column in the row
                IWebElement languageElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement languageLevel = row.FindElement(By.XPath("./td[2]"));
                string languageText = languageElement.Text;
                string languageLevelText = languageLevel.Text;

                // Check if the language matches the provided text
                if (languageText.Equals(language, StringComparison.OrdinalIgnoreCase) && languageLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on delete icon button of desired row
                    IWebElement languageDeleteIcon = row.FindElement(By.XPath("./td[3]/span[2]/i"));
                    languageDeleteIcon.Click();

                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;

                    break;
                }
            }
        }
        public string GetDeleteLanguageResult(string language, string level)
        {
            //Return language deleted result
            Wait.WaitToExist(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]", 6);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "";

            foreach (IWebElement row in rows)
            {
                // Get the text of the language and level column in the row
                IWebElement languageElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement languageLevel = row.FindElement(By.XPath("./td[2]"));
                string languageText = languageElement.Text;
                string languageLevelText = languageLevel.Text;

                // Check if the language matches the provided text
                if (languageText.Equals(language, StringComparison.OrdinalIgnoreCase) && languageLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    result = "Not Deleted";

                    break;
                }
                else
                {
                    result = "Deleted";
                }
            }
            return result;
        }
        public void CancelLanguageAddition(string language, string languageLevel)
        {
            //----Cancel adding a language-----------

            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Language']/following-sibling::th[2]/div", 5);

            //Click on "Add New" button
            AddNewLanguageButton.Click();

            //Enter language and select language level
            LanguageTextBox.SendKeys(language);

            SelectElement languageLevelOption = new SelectElement(LanguageLevelDropDown);
            languageLevelOption.SelectByText(languageLevel);

            //Click on "Cancel" button
            CancelButton.Click();
        }
        public void CancelLanguageUpdation(string existingLanguage, string existingLevel, string language, string Level)
        {

            //---------Cancel updating a Language-----

            Wait.WaitToExist(driver, "XPath", "//th[text()='Language']//ancestor::thead/following-sibling::tbody[last()]", 6);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Language']//ancestor::thead/following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                // Get the text of the language and level column in the row
                IWebElement languageElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement languageLevel = row.FindElement(By.XPath("./td[2]"));
                string languageText = languageElement.Text;
                string languageLevelText = languageLevel.Text;

                // Check if the language matches the provided text
                if (languageText.Equals(existingLanguage, StringComparison.OrdinalIgnoreCase) && languageLevelText.Equals(existingLevel, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on update icon button of desired row

                    IWebElement languageUpdateIcon = row.FindElement(By.XPath("./td[3]/span[1]/i"));
                    languageUpdateIcon.Click();

                    //Clear textbox
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Add Language']", 5);
                    LanguageTextBox.SendKeys(Keys.Control + "A");
                    LanguageTextBox.SendKeys(Keys.Backspace);

                    //Enter laguage and language level
                    LanguageTextBox.SendKeys(language);

                    SelectElement languageLevelOption = new SelectElement(LanguageLevelDropDown);
                    languageLevelOption.SelectByText(Level);

                    //Click on "Cancel" button.            
                    CancelButton.Click();

                    break;
                }
            }
        }
        public void AddSkill(string skill, string skillLevel)
        {
            //----Adding Skill------------

            try
            {
                Wait.WaitToBeClickable(driver, "XPath", "//*[@class='ns-close']", 4);
                CloseMessageIcon.Click();
            }
            catch (Exception ex)
            {
                var exception = ex;
            }

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 5);

            //Click on "Skills" tab.
            SkillTab.Click();

            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Skill']/following-sibling::th[2]/div", 4);
            AddNewSkillButton.Click();

            //Enter skill and select skill level
            SkillTextBox.SendKeys(skill);

            SelectElement skillLevelOption = new SelectElement(SkillLevelDropDown);
            skillLevelOption.SelectByText(skillLevel);

            //Click on "Add" button.
            AddButton.Click();

            //Saving error or success message
            Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
            Message = MessageWindow.Text;
        }
        public string GetSkill()
        {
            //Return new added skill
            return NewSkillAddedCell.Text;
        }
        public string GetSkillLevel()
        {
            //Return new added skill level
            return NewSkillLevelAddedCell.Text;
        }
        public void UpdateSkill(string existingSkill, string existingLevel, string skill, string level)
        {
            //---------updating a Skill-----

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            Wait.WaitToExist(driver, "XPath", "//th[text()='Skill']//ancestor::thead//following-sibling::tbody[last()]", 5);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Skill']//ancestor::thead/following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                // Get the text of the skill and level column in the row
                IWebElement skillElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement skillLevel = row.FindElement(By.XPath("./td[2]"));
                string skillText = skillElement.Text;
                string skillLevelText = skillLevel.Text;

                // Check if the skill matches the provided text
                if (skillText.Equals(existingSkill, StringComparison.OrdinalIgnoreCase) && skillLevelText.Equals(existingLevel, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on update icon button of desired row

                    IWebElement skillUpdateIcon = row.FindElement(By.XPath("./td[3]/span[1]/i"));
                    skillUpdateIcon.Click();

                    //Clear textbox
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Add Skill']", 5);
                    SkillTextBox.SendKeys(Keys.Control + "A");
                    SkillTextBox.SendKeys(Keys.Backspace);

                    SkillTextBox.SendKeys(skill);

                    SelectElement skillLevelOption = new SelectElement(SkillLevelDropDown);
                    skillLevelOption.SelectByText(level);

                    //Click on "Update" button
                    UpdateButton.Click();

                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;

                    if (GetMessage() == "Please enter skill and experience level")
                    {
                        CancelButton.Click();
                    }
                    break;
                }
            }
        }
        public string GetUpdatedSkillResult(string skill, string level)
        {
            //Return skill updated result

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            Wait.WaitToExist(driver, "XPath", "//th[text()='Skill']//ancestor::thead//following-sibling::tbody[last()]", 5);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Skill']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "";

            foreach (IWebElement row in rows)
            {
                // Get the text of the skill and level column in the row
                IWebElement skillElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement skillLevel = row.FindElement(By.XPath("./td[2]"));
                string skillText = skillElement.Text;
                string skillLevelText = skillLevel.Text;

                // Check if the skill matches the provided text
                if (skillText.Equals(skill, StringComparison.OrdinalIgnoreCase) && skillLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    result = "Updated";
                    break;
                }
                else
                {
                    result = "Not updated";
                }
            }
            return result;
        }
        public void DeleteSkill(string skill, string level)
        {
            //---Deleting a skill-----
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            Wait.WaitToExist(driver, "XPath", "//th[text()='Skill']//ancestor::thead//following-sibling::tbody[last()]", 5);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Skill']//ancestor::thead/following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                // Get the text of the skill and level column in the row
                IWebElement skillElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement skillLevel = row.FindElement(By.XPath("./td[2]"));
                string skillText = skillElement.Text;
                string skillLevelText = skillLevel.Text;

                // Check if the skill matches the provided text
                if (skillText.Equals(skill, StringComparison.OrdinalIgnoreCase) && skillLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on delete icon button of desired row
                    IWebElement skillDeleteIcon = row.FindElement(By.XPath("./td[3]/span[2]/i"));
                    skillDeleteIcon.Click();

                    //Saving error or success message
                    Wait.WaitToExist(driver, "XPath", "//div[@class='ns-box-inner']", 4);
                    Message = MessageWindow.Text;
                }
            }
        }
        public string GetDeletedSkillResult(string skill, string level)
        {
            //Return skill deleted result
            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            Wait.WaitToExist(driver, "XPath", "//th[text()='Skill']//ancestor::thead//following-sibling::tbody[last()]", 5);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Skill']//ancestor::thead/following-sibling::tbody/tr"));
            string result = "";
            foreach (IWebElement row in rows)
            {
                // Get the text of the skill and level column in the row
                IWebElement skillElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement skillLevel = row.FindElement(By.XPath("./td[2]"));
                string skillText = skillElement.Text;
                string skillLevelText = skillLevel.Text;

                // Check if the skill matches the provided text
                if (skillText.Equals(skill, StringComparison.OrdinalIgnoreCase) && skillLevelText.Equals(level, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on delete icon button of desired row
                    result = "Not Deleted";
                }
                else
                {
                    result = "Deleted";
                }
            }
            return result;
        }
        public void CancelSkillAddition(string skill, string skillLevel)
        {
            //----Cancel adding skill------------

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            //Click on "Add New" button.
            Wait.WaitToBeClickable(driver, "XPath", "//*[text()='Skill']/following-sibling::th[2]/div", 4);
            AddNewSkillButton.Click();

            //Enter skill and select skill level.
            SkillTextBox.SendKeys(skill);

            SelectElement skillLevelOption = new SelectElement(SkillLevelDropDown);
            skillLevelOption.SelectByText(skillLevel);

            //Click on "Cancel" button
            CancelButton.Click();
        }
        public int DuplicateRecordCount(string item)
        {
            //Return duplicate language/skill count 
            var duplicateLanguageList = driver.FindElements(By.XPath("//td[text()='" + item + "']"));
            int result = duplicateLanguageList.Count;
            return result;
        }
        public void CancelSkillUpdation(string existingSkill, string existingLevel, string skill, string level)
        {
            //----Cancel updating a skill-----------

            Wait.WaitToBeClickable(driver, "XPath", "//a[text()='Skills']", 4);

            //Click on "Skills" tab.
            SkillTab.Click();

            Wait.WaitToExist(driver, "XPath", "//th[text()='Skill']//ancestor::thead//following-sibling::tbody[last()]", 5);

            // Find all rows in the table
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Skill']//ancestor::thead/following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                // Get the text of the skill and level column in the row
                IWebElement skillElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement skillLevel = row.FindElement(By.XPath("./td[2]"));
                string skillText = skillElement.Text;
                string skillLevelText = skillLevel.Text;

                // Check if the skill matches the provided text
                if (skillText.Equals(existingSkill, StringComparison.OrdinalIgnoreCase) && skillLevelText.Equals(existingLevel, StringComparison.OrdinalIgnoreCase))
                {
                    //Click on update icon button of desired row

                    IWebElement skillUpdateIcon = row.FindElement(By.XPath("./td[3]/span[1]/i"));
                    skillUpdateIcon.Click();

                    //Clear textbox
                    Wait.WaitToExist(driver, "XPath", "//*[@placeholder='Add Skill']", 5);
                    SkillTextBox.SendKeys(Keys.Control + "A");
                    SkillTextBox.SendKeys(Keys.Backspace);

                    SkillTextBox.SendKeys(skill);

                    SelectElement skillLevelOption = new SelectElement(SkillLevelDropDown);
                    skillLevelOption.SelectByText(level);

                    //Click on "Cancel" button
                    CancelButton.Click();

                    break;
                }
            }
        }
    }
}


