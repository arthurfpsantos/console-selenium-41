using ConsoleSelenium.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests.Pages;

/// <summary>
/// Page Object do formulário https://www.selenium.dev/selenium/web/web-form.html
/// </summary>
public class WebFormPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    private static readonly By CampoTexto = By.Name("my-text");
    private static readonly By CampoSenha = By.Name("my-password");
    private static readonly By CampoTextarea = By.Name("my-textarea");
    private static readonly By CampoReadonly = By.Name("my-readonly");
    private static readonly By CampoSelect = By.Name("my-select");
    private static readonly By CampoDatalist = By.Name("my-datalist");
    private static readonly By Checkbox1 = By.Id("my-check-1");
    private static readonly By Checkbox2 = By.Id("my-check-2");
    private static readonly By Radio1 = By.Id("my-radio-1");
    private static readonly By Radio2 = By.Id("my-radio-2");
    private static readonly By CampoCor = By.Name("my-colors");
    private static readonly By BotaoSubmit = By.CssSelector("button[type='submit']");
    private static readonly By MensagemResultado = By.Id("message");

    public WebFormPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, WebFormConfig.TempoMaximoDeEspera);
    }

    public WebFormPage Abrir()
    {
        _driver.Navigate().GoToUrl(WebFormConfig.Url);
        _wait.Until(d => d.FindElement(BotaoSubmit).Displayed);
        return this;
    }

    // --- Inputs de texto ---

    public WebFormPage PreencherTexto(string valor) => Preencher(CampoTexto, valor);

    public WebFormPage PreencherSenha(string valor) => Preencher(CampoSenha, valor);

    public WebFormPage PreencherTextarea(string valor) => Preencher(CampoTextarea, valor);

    public string ObterValorReadonly() => ObterValor(CampoReadonly);

    // --- Dropdown (select) e datalist ---

    public WebFormPage SelecionarDropdownPorTexto(string texto)
    {
        new SelectElement(_driver.FindElement(CampoSelect)).SelectByText(texto);
        return this;
    }

    public string ObterValorSelecionadoDropdown() =>
        new SelectElement(_driver.FindElement(CampoSelect)).SelectedOption.GetDomProperty("value") ?? string.Empty;

    public WebFormPage PreencherDatalist(string cidade) => Preencher(CampoDatalist, cidade);

    public string ObterValorDatalist() => ObterValor(CampoDatalist);

    // --- Checkboxes e radios ---

    public bool Checkbox1EstaMarcado() => _driver.FindElement(Checkbox1).Selected;

    public bool Checkbox2EstaMarcado() => _driver.FindElement(Checkbox2).Selected;

    public WebFormPage MarcarCheckbox2()
    {
        var checkbox = _driver.FindElement(Checkbox2);
        if (!checkbox.Selected)
        {
            checkbox.Click();
        }
        return this;
    }

    public bool Radio1EstaSelecionado() => _driver.FindElement(Radio1).Selected;

    public bool Radio2EstaSelecionado() => _driver.FindElement(Radio2).Selected;

    public WebFormPage SelecionarRadio2()
    {
        _driver.FindElement(Radio2).Click();
        return this;
    }

    // --- Color picker ---

    public string ObterCor() => ObterValor(CampoCor);

    /// <summary>
    /// O seletor nativo de cor não aceita SendKeys no Chrome, então o valor é
    /// definido via JavaScript e os eventos input/change são disparados.
    /// </summary>
    public WebFormPage AlterarCor(string corHex)
    {
        var campo = _driver.FindElement(CampoCor);
        ((IJavaScriptExecutor)_driver).ExecuteScript(
            "arguments[0].value = arguments[1];" +
            "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
            "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
            campo, corHex);
        return this;
    }

    // --- Envio ---

    public WebFormPage Submeter()
    {
        _driver.FindElement(BotaoSubmit).Click();
        return this;
    }

    public string ObterMensagemDeResultado() =>
        _wait.Until(d => d.FindElement(MensagemResultado)).Text;

    // --- Utilitários ---

    private WebFormPage Preencher(By localizador, string valor)
    {
        var campo = _driver.FindElement(localizador);
        campo.Clear();
        campo.SendKeys(valor);
        return this;
    }

    private string ObterValor(By localizador) =>
        _driver.FindElement(localizador).GetDomProperty("value") ?? string.Empty;
}
