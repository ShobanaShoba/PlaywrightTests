/*using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTests;

[TestFixture]
public class GoogleTest
{
    [Test]
    public async Task OpenGoogle()
    {
        using var playwright = await Playwright.CreateAsync();

        // Launch browser visibly
        await using var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = false
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://demo.snipeitapp.com/login");
        // Fill in username and password
        await page.FillAsync("input[name='username']", "admin");   // Replace with your username
        await page.FillAsync("input[name='password']", "password");   // Replace with your password
        // Click login button
        await page.ClickAsync("button[type='submit']");

        //System.Console.WriteLine("Browser opened!");

        await Task.Delay(200); // Keep browser open until manual close
        
    }
}
*/