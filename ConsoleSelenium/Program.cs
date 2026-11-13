using ConsoleSelenium.Config;

Console.WriteLine("=== ConsoleSelenium41 - Automação do Web Form ===");
Console.WriteLine($"Formulário alvo: {WebFormConfig.Url}");
Console.WriteLine();
Console.WriteLine("Cenários automatizados (projeto ConsoleSelenium.Tests):");
Console.WriteLine("  1. Cadastro completo (text, password, textarea, readonly)");
Console.WriteLine("  2. Dropdown (select) e datalist");
Console.WriteLine("  3. Checkboxes, radios e color picker");
Console.WriteLine();
Console.WriteLine("Para executar os testes: dotnet test");
