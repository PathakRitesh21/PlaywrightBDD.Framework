using Microsoft.Playwright;

public class PlaywrightDriver
{
    public IPlaywright? Playwright { get; private set; }
    public IBrowser? Browser { get; private set; }
    public IPage? Page { get; private set; }

    public async Task Init()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var context = await Browser.NewContextAsync();
        Page = await context.NewPageAsync();
    }

    public async Task Quit()
    {
        await Browser!.CloseAsync();
        Playwright!.Dispose();
    }
}
