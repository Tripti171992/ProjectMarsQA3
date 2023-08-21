using Mars1.Utilities;
using TechTalk.SpecFlow;

namespace Mars1.Hooks
{
    [Binding]
    public sealed class HookHook : CommonDriver
    {
        [BeforeScenario]
        public void BeforeScenarioWithTag()
        {
            //Launching a browser
            Initialize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //Closing the browser
             driver.Close();
        }
    }
}