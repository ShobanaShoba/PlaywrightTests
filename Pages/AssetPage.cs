using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightTests.TestData;

namespace PlaywrightTests.Page
{
    public class AssetPage
    {
        private readonly IPage _page;
        public AssetPage(IPage page) => _page = page;

        // Helper to select from a Select2 dropdown
        private async Task SelectFromSelect2Async(string dropdownSelector, string keyword)
        {
            // Click the dropdown to open
            var dropdown = _page.Locator(dropdownSelector);
            await dropdown.ClickAsync();

            // Wait for the search input to appear
            var searchInput = _page.Locator("input.select2-search__field");
            await searchInput.WaitForAsync(new() { State = Microsoft.Playwright.WaitForSelectorState.Visible });

            // Type the keyword from AssetData
            await searchInput.FillAsync(keyword);

            // Wait for the matching option and click
            var option = _page.Locator(".select2-results__option", new() { HasTextString = keyword }).First;
            await option.WaitForAsync(new() { State = Microsoft.Playwright.WaitForSelectorState.Visible });
            await option.ClickAsync();
        }

        public async Task<string> CreateAssetAsync(string assetName, string v, string assetType)
        {
            try
            {
                // Model Dropdown
                await SelectFromSelect2Async("#select2-model_select_id-container.needsclick", AssetData.Model);

                // Status Dropdown
                await SelectFromSelect2Async("#select2-status_select_id-container.needsclick", AssetData.Status);

                // Save the asset
                await _page.Locator("button:has-text('Save')").Nth(1).ClickAsync();

                // Wait for a success notification
                var notification = await _page.Locator(".alert-success").TextContentAsync();
                return notification ?? "Asset created successfully";
            }
            catch (Exception ex)

            {
                return $"Failed: {ex.Message}";
            }
        }
    }
}
