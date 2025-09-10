using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Pages;
using System.Threading.Tasks;
using PlaywrightTests.TestData;

namespace PlaywrightTests.Tests
{
    [TestFixture]
    public class LoginTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private LoginPage _loginPage;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
            _page = await _browser.NewPageAsync();
            _loginPage = new LoginPage(_page);
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.EnterUsernameAsync(LoginData.Username);
            await _loginPage.EnterPasswordAsync(LoginData.Password);
            await _loginPage.ClickLoginAsync();

            // Wait for dashboard page title <h1 class="pull-left pagetitle">
            var pageTitle = _page.Locator("h1.pull-left.pagetitle");
            await pageTitle.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            // Assertion
            Assert.That(await pageTitle.IsVisibleAsync(), Is.True, "Login failed: Dashboard title not visible");
            Assert.That(await pageTitle.InnerTextAsync(), Does.Contain("Dashboard").IgnoreCase, "Unexpected page title");


            // wait 3 seconds to see result
            await Task.Delay(3000);
        }

        [Test]
        public async Task NegativeLoginTest()
        {
            await _page.GotoAsync("https://demo.snipeitapp.com/login");

            // Enter invalid credentials
            await _page.FillAsync("input[name='username']", LoginData.InvalidUsername);
            await _page.FillAsync("input[name='password']", LoginData.InvalidPassword);

            // Click login
            await _page.ClickAsync("button[type='submit']");

            // Assert error message is visible
            //locate the error div
            var errorMessage = _page.Locator("div.alert.alert-danger.fade.in");

            // wait until it appears
            await errorMessage.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            // assert
            Assert.That(await errorMessage.IsVisibleAsync(), Is.True, "Error message not shown for invalid login.");

            await Task.Delay(3000);
        }

        [Test]
        public async Task LoginWithBlankPassword()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.EnterUsernameAsync(LoginData.Username); // valid username
            await _loginPage.EnterPasswordAsync(LoginData.blank_Username); // blank password
            await _loginPage.ClickLoginAsync();

                // Wait for error alert
            var errorAlert = _page.Locator("div.alert.alert-danger.fade.in");
            await errorAlert.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            // Assertion: check the text for blank password case
            var errorText = await errorAlert.InnerTextAsync();
            Assert.That(errorText, Does.Contain("Error: Please check the form below").IgnoreCase,
                $"Unexpected error text: {errorText}");
        }
        [Test]
        public async Task LoginWithBlankUsername()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.EnterUsernameAsync(""); // blank username
            await _loginPage.EnterPasswordAsync(LoginData.Password); // valid password
            await _loginPage.ClickLoginAsync();

            // Wait for error alert
            var errorAlert = _page.Locator("div.alert.alert-danger.fade.in");
            await errorAlert.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            var errorText = await errorAlert.InnerTextAsync();
            Assert.That(errorText, Does.Contain("Error: Please check the form below").IgnoreCase,
                $"Unexpected error text: {errorText}");
        }

        [Test]
        public async Task LoginWithBlankUsernameAndPassword()
        {
            await _loginPage.NavigateAsync();
            await _loginPage.EnterUsernameAsync(""); // blank username
            await _loginPage.EnterPasswordAsync(""); // blank password
            await _loginPage.ClickLoginAsync();

            // Wait for error alert
            var errorAlert = _page.Locator("div.alert.alert-danger.fade.in");
            await errorAlert.WaitForAsync(new() { State = WaitForSelectorState.Visible });

            var errorText = await errorAlert.InnerTextAsync();
            Assert.That(errorText, Does.Contain("Error: Please check the form below").IgnoreCase,
                $"Unexpected error text: {errorText}");
        }


        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
            
            // Capture screenshot if test failed
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
{
    var utilsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Utils");
    Directory.CreateDirectory(utilsDir);

    var screenshotPath = Path.Combine(
        utilsDir,
        $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
    );

    await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
    TestContext.Out.WriteLine($"Screenshot saved: {screenshotPath}");
}

        }
    }
}

