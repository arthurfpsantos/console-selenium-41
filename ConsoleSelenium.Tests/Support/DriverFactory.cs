using ConsoleSelenium.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleSelenium.Tests.Support;

/// <summary>
/// Cria instâncias do ChromeDriver. O Selenium Manager baixa o driver compatível automaticamente.
/// </summary>
public static class DriverFactory
{
    public static IWebDriver CriarChrome()
    {
        var options = new ChromeOptions();

        if (WebFormConfig.ExecutarSemInterface)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--window-size=1366,900");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        return new ChromeDriver(options);
    }
}
