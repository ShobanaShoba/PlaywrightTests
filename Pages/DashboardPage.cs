using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Page
{
    public class DashboardPage(IPage page)
    {
        private readonly IPage _page = page;

        public async Task GoToCreateAssetAsync()
        {
            // Dashboard code for Click the dropdown menu and select "Asset"
            await _page.ClickAsync("a.dropdown-toggle");
            await _page.ClickAsync("ul.dropdown-menu >> text='Asset'");
        }

        public async Task GoToListAllAsync()
        {
            // Click barcode button to open dropdown
            await _page.ClickAsync("i.fas.fa-barcode.fa-fw");
     await Task.Delay(3000);


        }

    }
}
