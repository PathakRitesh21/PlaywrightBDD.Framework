using Reqnroll;
using NUnit.Framework;

[Binding]
public class LoginWithExcelSteps
{
    private readonly GtplLoginPage _loginPage;
    private string? _username;
    private string? _password;

    public LoginWithExcelSteps()
    {
        _loginPage = new GtplLoginPage(Hooks.Driver.Page!);
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
        await _loginPage.Navigate();
        await _loginPage.Login(_username!, _password!);
    }

    [Then("user should be logged in successfully with data from Excel")]
    public async Task ThenUserShouldBeLoggedIn()
    {
        await ScreenshotUtils.TakeScreenshotAsync(Hooks.Driver.Page!, "AfterLogin");
        Assert.That(await _loginPage.IsLoggedIn(), Is.True);
    }
}
