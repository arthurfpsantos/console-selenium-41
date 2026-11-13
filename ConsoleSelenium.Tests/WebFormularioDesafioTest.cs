using ConsoleSelenium.Config;
using ConsoleSelenium.Tests.Pages;
using ConsoleSelenium.Tests.Support;
using OpenQA.Selenium;

namespace ConsoleSelenium.Tests;

[TestClass]
public sealed class WebFormularioDesafioTest
{
    private IWebDriver _driver = null!;
    private WebFormPage _pagina = null!;

    [TestInitialize]
    public void Setup()
    {
        _driver = DriverFactory.CriarChrome();
        _pagina = new WebFormPage(_driver).Abrir();
    }

    [TestCleanup]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    /// <summary>
    /// Exercício 1: Cadastro completo com preenchimento misto.
    /// </summary>
    [TestMethod]
    public void Deve_PreencherCadastroCompleto_E_ExibirMensagemDeSucesso()
    {
        _pagina
            .PreencherTexto(DadosDeTeste.NomeCompleto)
            .PreencherSenha(DadosDeTeste.SenhaSegura)
            .PreencherTextarea(DadosDeTeste.DescricaoExperiencia);

        Assert.AreEqual(WebFormConfig.ValorPadraoReadonly, _pagina.ObterValorReadonly(),
            "O campo readonly deve manter o valor padrão.");

        var mensagem = _pagina.Submeter().ObterMensagemDeResultado();

        Assert.AreEqual(WebFormConfig.MensagemDeSucesso, mensagem);
    }

    /// <summary>
    /// Exercício 2: Dropdown (SelectElement) e datalist.
    /// </summary>
    [TestMethod]
    public void Deve_SelecionarDropdownEDatalist_E_SubmeterSemExcecoes()
    {
        _pagina.SelecionarDropdownPorTexto(DadosDeTeste.OpcaoDropdownTexto);

        Assert.AreEqual(DadosDeTeste.OpcaoDropdownValor, _pagina.ObterValorSelecionadoDropdown(),
            "A opção 'Two' deve ter o valor 2.");

        _pagina.PreencherDatalist(DadosDeTeste.Cidade);

        Assert.AreEqual(DadosDeTeste.Cidade, _pagina.ObterValorDatalist());

        string mensagem = string.Empty;
        try
        {
            mensagem = _pagina.Submeter().ObterMensagemDeResultado();
        }
        catch (WebDriverException ex)
        {
            Assert.Fail($"O envio do formulário lançou uma exceção: {ex.Message}");
        }

        Assert.AreEqual(WebFormConfig.MensagemDeSucesso, mensagem);
    }

    /// <summary>
    /// Exercício 3: Checkboxes, radios e color picker.
    /// </summary>
    [TestMethod]
    public void Deve_AlternarCheckboxesRadiosECor_E_SubmeterComSucesso()
    {
        Assert.IsTrue(_pagina.Checkbox1EstaMarcado(), "O checkbox 1 deve vir marcado por padrão.");

        _pagina.MarcarCheckbox2();
        Assert.IsTrue(_pagina.Checkbox2EstaMarcado(), "O checkbox 2 deve ficar marcado após o clique.");

        _pagina.SelecionarRadio2();
        Assert.IsTrue(_pagina.Radio2EstaSelecionado(), "O radio 2 deve estar selecionado.");
        Assert.IsFalse(_pagina.Radio1EstaSelecionado(), "O radio 1 deve ser desmarcado.");

        var corOriginal = _pagina.ObterCor();
        _pagina.AlterarCor(DadosDeTeste.NovaCor);

        Assert.AreNotEqual(corOriginal, _pagina.ObterCor(), "A cor deve ter sido alterada.");
        Assert.AreEqual(DadosDeTeste.NovaCor, _pagina.ObterCor());

        var mensagem = _pagina.Submeter().ObterMensagemDeResultado();

        Assert.AreEqual(WebFormConfig.MensagemDeSucesso, mensagem);
    }
}
