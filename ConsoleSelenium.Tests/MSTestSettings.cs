// Cada teste abre o seu próprio navegador; execução sequencial evita instabilidade.
[assembly: Parallelize(Workers = 1, Scope = ExecutionScope.MethodLevel)]
