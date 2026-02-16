using Reqnroll;

[Binding]
public class Hooks
{
    private static PlaywrightDriver? _driver;
    private static bool _runFolderInitialized = false;
    private readonly ScenarioContext _scenarioContext;

    public Hooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        ScreenshotUtils.InitRunFolder();
        _runFolderInitialized = true;
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
        // Take screenshot only if scenario failed (you can change this behavior)
        if (_scenarioContext.TestError != null)
        {
            await ScreenshotUtils.TakeScreenshotAsync(
                _driver!.Page!,
                "FAILED_" + _scenarioContext.ScenarioInfo.Title
            );
        }

        await _driver!.Quit();
    }

    public static PlaywrightDriver Driver => _driver!;
}
