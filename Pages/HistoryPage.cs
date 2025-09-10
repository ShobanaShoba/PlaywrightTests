/*using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Page
{
    public class HistoryPage
    {
        private readonly IPage _page;

        public HistoryPage(IPage page) => _page = page;

        /// <summary>
        /// Clicks the History tab on the asset details page.
        /// </summary>
        public async Task ClickHistoryTabAsync()
        {
            // Click the History tab (use a stable selector)
            await _page.ClickAsync("span.hidden-xs.hidden-sm");

            // Wait for the history table to appear
            await _page.WaitForSelectorAsync("#history");
            //await _page.ClickAsync("//*[@id='webui']/div/div[1]/div/ul/li[5]/a/span[1]/i");
            //await _page.ClickAsync("text=History");
        }

        /// <summary>
        /// Gets the latest history entry text from the history table.
        /// </summary>
        public async Task<string> GetLatestHistoryEntryAsync()
        {
            var rows = await _page.QuerySelectorAllAsync("text=History");
            if (rows.Count == 0)
                throw new System.Exception("No history entries found.");

            return await rows[0].InnerTextAsync();
        }
    }
}
*/
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Page
{
    public class HistoryPage
    {
        private readonly IPage _page;

        public HistoryPage(IPage page) => _page = page;

        /// <summary>
        /// Clicks the History tab on the asset details page.
        /// </summary>
        public async Task ClickHistoryTabAsync()
        {
            
            var navTabs = _page.Locator("ul.nav.nav-tabs.hidden-print");

            // Select the 4th li (0-based index: 3)
            var historyTab = navTabs.Locator("li").Nth(3);

            // Scroll, wait, and click
            await historyTab.ScrollIntoViewIfNeededAsync();
            await historyTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await historyTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached });
            await historyTab.ClickAsync();

            // Wait for the history table to appear
            await _page.WaitForSelectorAsync("table#history tbody tr");
            
        }

    
        public async Task<string> GetLatestHistoryEntryAsync()
        {
            var rows = await _page.QuerySelectorAllAsync("table.history tbody tr");
            if (rows.Count == 0)
                throw new System.Exception("No history entries found.");

            return await rows[0].InnerTextAsync();
        }
    }
}
