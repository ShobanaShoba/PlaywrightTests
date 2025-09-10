using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        // Page selectors
        private string UsernameField => "#username";
        private string PasswordField => "#password";
        private string LoginButton => "button[type='submit']";

        // Actions
        public async Task NavigateAsync() => await _page.GotoAsync("https://demo.snipeitapp.com/login");

        public async Task EnterUsernameAsync(string username) => await _page.FillAsync(UsernameField, username);

        public async Task EnterPasswordAsync(string password) => await _page.FillAsync(PasswordField, password);

        public async Task ClickLoginAsync() => await _page.ClickAsync(LoginButton);

        // Combined login method
        public async Task LoginAsync(string username, string password)
        {
            await NavigateAsync();
            await EnterUsernameAsync(username);
            await EnterPasswordAsync(password);
            await ClickLoginAsync();

            // Optional: Wait for dashboard to ensure login succeeded
            await _page.WaitForSelectorAsync("h1.pull-left.pagetitle");
        }
    }
}
