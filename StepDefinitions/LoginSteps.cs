using Io.Cucumber.Messages.Types;
using NUnit.Framework;
using Reqnroll;

[Binding]
public class LoginSteps
{
    private readonly GtplLoginPage? _loginPage;

    public LoginSteps()
    {
        var driver = Hooks.Driver;
        _loginPage = driver != null ? new GtplLoginPage(driver.Page!) : null;
    }

    [Given("user navigates to GTPL login page")]
    public async Task GivenUserNavigates()
    {
        Assert.That(_loginPage, Is.Not.Null, "Login page not initialized - Playwright driver is null");
        await _loginPage!.Navigate();
    }

    [When(@"user logs in with username ""(.*)"" and password ""(.*)""")]
    public async Task WhenUserLogsIn(string user, string pass)
    {
        Assert.That(_loginPage, Is.Not.Null, "Login page not initialized - Playwright driver is null");
        await _loginPage!.Login(user, pass);
    }

    [Then("user should be logged in successfully")]
    public async Task ThenUserShouldBeLoggedIn()
    {
        Assert.That(_loginPage, Is.Not.Null, "Login page not initialized - Playwright driver is null");
        await ScreenshotUtils.TakeScreenshotAsync(Hooks.Driver!.Page!, "AfterLogin");
        Assert.That(await _loginPage!.IsLoggedIn(), Is.True);

    }
}
