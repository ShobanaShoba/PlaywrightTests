using NUnit.Framework;
using PlaywrightTests.Page;
using PlaywrightTests.TestData;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightTests.Pages;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class ListSearchTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private DashboardPage? _dashboardPage;
        private ListPage? _listPage;

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
        public async Task SearchBySerialNumberAndVerify()
        {
            // Navigate to Asset creation page
            var dashboard = new DashboardPage(_page);
            await dashboard.GoToListAllAsync();

            //await _dashboardPage.GoToListAllAsync();

            string AssertTag = AssetData.AssertTag; // From TestData
            string expectedModel = AssetData.Model;
            string expectedStatus = AssetData.Status;

            _listPage = new ListPage(_page); // Ensure _listPage is initialized
            //await _listPage.SearchBySerialNumberAsync(AssertTag);
            //find assert create test latter


            // Locate the row that contains  unique ID
            var row = _page.Locator("table tr", new PageLocatorOptions { HasText = AssertTag });

            // Get specific cells inside that row
            string assetId = await row.Locator("td").Nth(3).InnerTextAsync();   // 1st column
            string model   = await row.Locator("td").Nth(5).InnerTextAsync();   // 3rd column
            string status  = await row.Locator("td").Nth(7).InnerTextAsync();   // 5th column

            // Now validate
            Assert.That(assetId, Is.EqualTo(AssertTag), "Asset ID mismatch!");
            Assert.That(model, Is.EqualTo(expectedModel), "Model mismatch!");
            Assert.That(status, Is.EqualTo(expectedStatus), "Status mismatch!");


        }   

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
