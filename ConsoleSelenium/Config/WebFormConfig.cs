namespace ConsoleSelenium.Config;

/// <summary>
/// Configurações centrais da automação do formulário web do Selenium.
/// </summary>
public static class WebFormConfig
{
    public const string Url = "https://www.selenium.dev/selenium/web/web-form.html";

    public const string MensagemDeSucesso = "Received!";

    public const string ValorPadraoReadonly = "Readonly input";

    public static readonly TimeSpan TempoMaximoDeEspera = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Defina a variável de ambiente HEADLESS=false para ver o navegador abrindo.
    /// </summary>
    public static bool ExecutarSemInterface =>
        !string.Equals(Environment.GetEnvironmentVariable("HEADLESS"), "false", StringComparison.OrdinalIgnoreCase);
}
