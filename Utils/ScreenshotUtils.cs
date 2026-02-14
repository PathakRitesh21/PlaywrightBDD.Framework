using Microsoft.Playwright;

public static class ScreenshotUtils
{
    private static readonly string ScreenshotFolder = "Screenshots";

    public static async Task TakeScreenshotAsync(IPage page, string namePrefix, bool fullPage = true)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        Directory.CreateDirectory(ScreenshotFolder);

        var fileName = $"{namePrefix}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(ScreenshotFolder, fileName);

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = filePath,
            FullPage = fullPage
        });

        Console.WriteLine($"📸 Screenshot saved: {filePath}");
    }
}
