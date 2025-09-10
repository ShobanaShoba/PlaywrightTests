using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Page;
using PlaywrightTests.Pages;
using PlaywrightTests.TestData;
using System;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class AssetCreationTests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            // Initialize Playwright and launch browser
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            // Perform login
            var loginPage = new LoginPage(_page);
            await loginPage.LoginAsync("admin", "password");

        }

        
        [Test]
        public async Task CreateNewMacbookAsset()
        {
            // Navigate to Asset creation page
            var dashboard = new DashboardPage(_page);
            await dashboard.GoToCreateAssetAsync();

            // Wait for page to load
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            // Create Asset
            var assetPage = new AssetPage(_page);
            //var resultMessage = await assetPage.CreateAssetAsync();

            // Assertion
            var successAlert = _page.Locator(".alert.alert-success.fade.in");
            await successAlert.WaitForAsync(new LocatorWaitForOptions { Timeout = 500 });
            
            
        }

        [Test]
        public async Task CreateAsset_ShouldFail_WhenCompanyIsInvalid()
        {
            // Navigate to Asset creation page
            var dashboard = new DashboardPage(_page);
            await dashboard.GoToCreateAssetAsync();

            // Wait for page to load
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
             // Use invalid asset data
                //ssetData.Company = "Invalid Company";
                AssetData.Model = "Macbook Pro 13";
                AssetData.Status = "Ready to Deploy";
                AssetData.AssertTag = "";

                //await _assetPage.CreateAssetAsync();

                // Try to find success message, expecting failure
                var successMessage = _page.Locator("div.alert alert-danger fade in");
                bool messageVisible = await successMessage.IsVisibleAsync();

                var errorAlert = _page.Locator(".alert.alert-danger.fade.in");
                await errorAlert.WaitForAsync(new LocatorWaitForOptions { Timeout = 500 });

                //var errorText = await errorAlert.InnerTextAsync();
               // Assert.That(errorText, Does.Contain("Error: Please check the form below").IgnoreCase,
               //     $"Unexpected error text: {errorText}");

                
            }
            

        [TearDown]
        public async Task TearDown()
        {
            // Close browser and Playwright
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}

