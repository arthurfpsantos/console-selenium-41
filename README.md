# console-selenium-41

Lista de exercícios de **Garantia da Qualidade de Software** (Prof. Daniel Henrique Matos de Paiva): automação do formulário
[web-form.html do Selenium](https://www.selenium.dev/selenium/web/web-form.html) usando **C#, .NET 10, MSTest e Selenium WebDriver**.

## Estrutura

```
ConsoleSelenium41.slnx
├── ConsoleSelenium/               # Console Application (.NET 10)
│   ├── Config/WebFormConfig.cs    # URL, mensagem de sucesso, timeouts
│   ├── Config/DadosDeTeste.cs     # Massa de dados dos cenários
│   └── Program.cs
└── ConsoleSelenium.Tests/         # MSTest + Selenium.WebDriver + Selenium.Support
    ├── Pages/WebFormPage.cs       # Page Object do formulário
    ├── Support/DriverFactory.cs   # Criação do ChromeDriver
    └── WebFormularioDesafioTest.cs
```

## Cenários

| Exercício | Teste | O que valida |
|---|---|---|
| 1 | `Deve_PreencherCadastroCompleto_E_ExibirMensagemDeSucesso` | Text, password e textarea preenchidos; readonly = "Readonly input"; página exibe "Received!" |
| 2 | `Deve_SelecionarDropdownEDatalist_E_SubmeterSemExcecoes` | `SelectElement` seleciona "Two" com valor 2; datalist recebe "Seattle"; envio sem exceções |
| 3 | `Deve_AlternarCheckboxesRadiosECor_E_SubmeterComSucesso` | Checkbox 1 marcado por padrão; checkbox 2 marcado; radio 2 selecionado; cor alterada; envio com sucesso |

## Como executar

Pré-requisitos: .NET SDK 10 e Google Chrome instalados (o Selenium Manager baixa o ChromeDriver automaticamente).

```bash
dotnet restore
dotnet test
```

Os testes rodam em modo headless por padrão. Para ver o navegador:

```bash
# PowerShell
$env:HEADLESS="false"; dotnet test
# bash
HEADLESS=false dotnet test
```
