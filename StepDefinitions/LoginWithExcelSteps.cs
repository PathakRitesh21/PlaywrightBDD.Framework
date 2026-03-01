using NUnit.Framework;
using Reqnroll;



[Binding]
public class LoginWithExcelSteps
{
    private readonly GtplLoginPage? _loginPage;
    private string? _username;
    private string? _password;

    public LoginWithExcelSteps()
    {
        var driver = Hooks.Driver;
        _loginPage = driver != null ? new GtplLoginPage(driver.Page!) : null;
    }

    [Given("user reads credentials from Excel")]
    public void GivenUserReadsExcel()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", "credentials.xlsx");
        _username = ExcelUtils.GetCellData(path, "Sheet1", 1, 0);
        _password = ExcelUtils.GetCellData(path, "Sheet1", 1, 1);
    }

    [When("user logs in using Excel data")]
    public async Task WhenUserLogsInUsingExcel()
    {
        Assert.That(_loginPage, Is.Not.Null, "Login page not initialized - Playwright driver is null");
        await _loginPage!.Navigate();
        await _loginPage!.Login(_username!, _password!);
    }

    [Then("user should be logged in successfully with data from Excel")]
    public async Task ThenUserShouldBeLoggedIn()
    {
        Assert.That(_loginPage, Is.Not.Null, "Login page not initialized - Playwright driver is null");
        await ScreenshotUtils.TakeScreenshotAsync(Hooks.Driver!.Page!, "AfterLogin");
        Assert.That(await _loginPage!.IsLoggedIn(), Is.True);
    }
}
