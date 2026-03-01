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

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        ScreenshotUtils.InitRunFolder();
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
        if (_driver != null)
        {
            await _driver.DisposeAsync();
        }
    }

    public static PlaywrightDriver Driver => _driver!;
}