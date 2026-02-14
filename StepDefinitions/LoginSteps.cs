using Io.Cucumber.Messages.Types;
using NUnit.Framework;
using Reqnroll;

[Binding]
public class LoginSteps
{
    private readonly GtplLoginPage _loginPage;

    public LoginSteps()
    {
        _loginPage = new GtplLoginPage(Hooks.Driver.Page!);

    }

    [Given("user navigates to GTPL login page")]
    public async Task GivenUserNavigates()
    {
        await _loginPage.Navigate();
    }

    [When(@"user logs in with username ""(.*)"" and password ""(.*)""")]
    public async Task WhenUserLogsIn(string user, string pass)
    {
        await _loginPage.Login(user, pass);
    }

    [Then("user should be logged in successfully")]
    public async Task ThenUserShouldBeLoggedIn()
    {
        await ScreenshotUtils.TakeScreenshotAsync(Hooks.Driver.Page!, "AfterLogin");
        Assert.That(await _loginPage.IsLoggedIn(), Is.True);

    }
}
