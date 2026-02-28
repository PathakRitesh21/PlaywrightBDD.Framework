using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Allure.Net.Commons;

public static class ScreenshotUtils
{
    // <project_root>\Screenshots
    private static readonly string RootScreenshotFolder =
        Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

    private static string? _currentRunFolder;

    // Call this once per test run (e.g., in BeforeTestRun)
    public static void InitRunFolder()
    {
        var runName = $"Run_{DateTime.Now:yyyyMMdd_HHmmss}";
        _currentRunFolder = Path.Combine(RootScreenshotFolder, runName);

        Directory.CreateDirectory(_currentRunFolder);

        Console.WriteLine($"📁 Screenshot run folder created: {_currentRunFolder}");
    }

    public static async Task TakeScreenshotAsync(
        IPage page,
        string namePrefix,
        bool fullPage = true)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        if (_currentRunFolder == null)
            throw new InvalidOperationException(
                "ScreenshotUtils.InitRunFolder() was not called.");

        var safeName = Regex.Replace(namePrefix, @"[^\w\-]", "_");
        var fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(_currentRunFolder, fileName);

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = filePath,
            FullPage = fullPage
        });

        Console.WriteLine($"📸 Screenshot saved: {filePath}");

        // 🔥 Attach to Allure Report
        if (File.Exists(filePath))
        {
            AllureApi.AddAttachment(
                safeName,
                "image/png",
                filePath
            );
        }
    }
}

