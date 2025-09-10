/*using NUnit.Framework;
using Microsoft.Playwright;
using PlaywrightTests.Pages;
using PlaywrightTests.TestData;
using System.Threading.Tasks;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class EndToEndTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _page = await _browser.NewPageAsync();
        }

        [Test]
        public async Task FullWorkflowTest()
        {
            // Initialize page objects using the _page instance
            var loginPage = new LoginPage(_page);
            var dashboard = new Page.DashboardPage(_page);
            var assetPage = new Page.AssetPage(_page);
            var listPage = new ListPage(_page);

            // Step 1: Login
            await loginPage.LoginAsync("admin", "password");

            // Step 2: Navigate to Asset creation page
            await dashboard.GoToCreateAssetAsync();
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            // Step 3: Create Asset (if needed)
            // await assetPage.CreateAssetAsync();

            // Step 4: Navigate to List All Assets
            await dashboard.GoToListAllAsync();

            // Step 5: Search for the asset
            string assetTag = AssetData.AssertTag;
            listPage.SearchBySerialNumber(assetTag);


            // Step 6: Locate the asset row
            var row = _page.Locator("#assetsListingTable tbody tr", new PageLocatorOptions { HasText = assetTag });

            // Step 7: Extract details
            string assetId = await row.Locator("td").Nth(3).InnerTextAsync();
            string model = await row.Locator("td").Nth(5).InnerTextAsync();
            string status = await row.Locator("td").Nth(7).InnerTextAsync();

            TestContext.Out.WriteLine($"Asset ID: {assetId}, Model: {model}, Status: {status}");

            // Step 8: Click the asset link in 4th column
            var targetLink = row.Locator("td:nth-child(4) a");
            await targetLink.ScrollIntoViewIfNeededAsync();
            await targetLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await targetLink.ClickAsync();
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Step 9: Open History page
            var historyIcon = _page.Locator("i.fas.fa-history.fa-2x");
            await historyIcon.ScrollIntoViewIfNeededAsync();
            await historyIcon.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await historyIcon.ClickAsync();

            // Step 10: Wait for history table
            await _page.WaitForSelectorAsync("table#history tbody tr");

            TestContext.Out.WriteLine("History table loaded successfully.");
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }

    internal class NewBaseType
    {
        internal void SearchBySerialNumber(string assertTag)
        {
            throw new NotImplementedException();
        }
    }

    internal class ListPage : NewBaseType
    {
        private IPage page;

        public ListPage(IPage page)
        {
            this.page = page;
        }

        internal void NewBaseTypeSearchBySerialNumber(string assetTag)
        {
            throw new NotImplementedException();
        }
    }
}*/
