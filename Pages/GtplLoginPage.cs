using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Playwright;

public class GtplLoginPage
{
    private readonly IPage _page;
    private const string Url = "https://demo.guru99.com/V1/index.php";

    public GtplLoginPage(IPage page)
    {
        _page = page;
    }

    public async Task Navigate()
    {
        await _page.GotoAsync(Url,
    new PageGotoOptions
    {
        Timeout = 60000,
        WaitUntil = WaitUntilState.Load
    });
    }

    public async Task Login(string user, string pass)
    {
        await _page.FillAsync("input[name='uid']", user);
        await _page.FillAsync("input[name='password']", pass);
        await _page.ClickAsync("input[name='btnLogin']");
    }

    public async Task<bool> IsLoggedIn()
    {
        try
        {
            // Wait a bit for navigation / page update after login
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Guru99 shows a "Log out" link when login succeeds
            var logoutLink = _page.Locator("text=Log out");

            // Wait up to 5 seconds for it to appear
            await logoutLink.WaitForAsync(new LocatorWaitForOptions
            {
                Timeout = 5000
            });

            return await logoutLink.IsVisibleAsync();
        }
        catch
        {
            return false;
        }
    }

}
