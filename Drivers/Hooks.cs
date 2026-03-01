using Reqnroll;
using Allure.Net.Commons;

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
        if (!_runFolderInitialized)
        {
            // Initialize Allure results directory and screenshot run folder
            Console.WriteLine("🔧 Initializing test run folders...");
            AllureLifecycle.Instance.CleanupResultDirectory();
            ScreenshotUtils.InitRunFolder();
            _runFolderInitialized = true;
        }
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        var scenarioTitle = _scenarioContext?.ScenarioInfo?.Title ?? "Unknown";
        Console.WriteLine($"\n▶️  Starting scenario: {scenarioTitle}");

        // Start Allure test case for Reqnroll scenario
        var testResult = new TestResult
        {
            uuid = Guid.NewGuid().ToString(),
            name = scenarioTitle,
            fullName = $"[Reqnroll] {scenarioTitle}",
            start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            historyId = scenarioTitle.GetHashCode().ToString()
        };

        AllureLifecycle.Instance.StartTestCase(testResult);

        // Only initialize Playwright for scenarios that need it (not for AllureVerification)
        if (!scenarioTitle.Contains("simple test") && !scenarioTitle.Contains("Allure"))
        {
            _driver = new PlaywrightDriver();
            await _driver.Init();
            Console.WriteLine("   ✓ Playwright driver initialized");
        }
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        var scenarioTitle = _scenarioContext?.ScenarioInfo?.Title ?? "Unknown";
        var hasPassed = _scenarioContext?.TestError == null;

        try
        {

            // Update Allure test case with completion info
            AllureLifecycle.Instance.UpdateTestCase(testCase =>
            {
                testCase.stop = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                testCase.description = $"BDD Scenario: {scenarioTitle}";
                testCase.descriptionHtml = $"<h3>Reqnroll BDD Scenario</h3><p>{scenarioTitle}</p>";
            });

            // Write the test case to disk
            AllureLifecycle.Instance.StopTestCase();
            AllureLifecycle.Instance.WriteTestCase();

            Console.WriteLine($"✅ Scenario {(hasPassed ? "PASSED" : "FAILED")}: {scenarioTitle}");

            if (_driver != null)
            {
                await _driver.DisposeAsync();
                _driver = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error in AfterScenario: {ex.Message}");

            // nothing to do for reporting here (Allure removed)
        }
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Console.WriteLine("\n✅ Test run completed!");
        Console.WriteLine("📊 Allure results written to: allure-results/");
    }

    public static PlaywrightDriver? Driver => _driver;
}
