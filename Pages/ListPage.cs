/*using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Page
{
    public class ListPage
    {
        private readonly IPage _page;

        public ListPage(IPage page) => _page = page;

        public async Task<string> GetLastRowTextAsync()
        {
            var rows = await _page.QuerySelectorAllAsync("table tbody tr");
            if (rows.Count == 0)
                return string.Empty;

            var lastRow = rows[rows.Count - 1];
            return await lastRow.InnerTextAsync();
        }

        public async Task SearchBySerialNumberAsync(string AssertTag)
        {
            // Correct way: combine multiple classes with dots
            await _page.FillAsync("input.form-control.search-input", AssertTag);

            // Press Enter to trigger the search
            await _page.PressAsync("input.form-control.search-input", "Enter");
            await Task.Delay(3000);
            // Optional: wait for search results to appear
            await _page.WaitForSelectorAsync("table tbody tr");
        }
    }
}
*/