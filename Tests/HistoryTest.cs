using NUnit.Framework;
using PlaywrightTests.Page;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightTests.Pages;
using PlaywrightTests.TestData;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class HistoryTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();

            var loginPage = new LoginPage(_page);
            await loginPage.LoginAsync("admin", "password");
        }

        [Test]
        public async Task ValidateAssetHistory()
        {
            // Step 1: Navigate to the list page and search for the asset
            var dashboard = new DashboardPage(_page);
            await dashboard.GoToListAllAsync();

            string assetTag = AssetData.AssertTag; // from test data
            var listPage = new ListPage(_page);
           // listPage.SearchBySerialNumberAsync(assetTag);


            // Locate the row containing the assetTag
            var row = _page.Locator("#assetsListingTable tbody tr", new PageLocatorOptions { HasText = assetTag });

            // Click the 4th column link (<a> inside td:nth-child(4))
            var targetLink = row.Locator("td:nth-child(4) a");
            await targetLink.ScrollIntoViewIfNeededAsync();
            await targetLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await targetLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
            await targetLink.ClickAsync();

            // Wait for page navigation to complete
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);


           
            var historyIcon = _page.Locator("i.fas.fa-history.fa-2x");

            // Ensure it is visible and enabled
            await historyIcon.ScrollIntoViewIfNeededAsync();
            await historyIcon.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await historyIcon.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });

            // Click the icon
            await historyIcon.ClickAsync();

            // Wait for the history table to appear
            await _page.WaitForSelectorAsync("table#history tbody tr");
            
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
