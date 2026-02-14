using Reqnroll;

[Binding]
public class Hooks
{
    private static PlaywrightDriver? _driver;
    private readonly ScenarioContext _scenarioContext;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _driver = new PlaywrightDriver();
        await _driver.Init();
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        // If scenario failed, take screenshot
        if (_scenarioContext.TestError != null)
        {
            await ScreenshotUtils.TakeScreenshotAsync(
                _driver!.Page!,
                _scenarioContext.ScenarioInfo.Title.Replace(" ", "_")
            );
        }

        await _driver!.Quit();
    }

    public static PlaywrightDriver Driver => _driver!;
}
