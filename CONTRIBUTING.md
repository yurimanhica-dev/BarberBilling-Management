# CONTRIBUTING.md - Guia de Contribuição

## 🎯 Por que contribuir?

Estamos construindo um sistema de alta qualidade para o ecossistema .NET. Sua contribuição ajuda a:

- Melhorar a segurança e performance
- Adicionar novas funcionalidades
- Corrigir bugs
- Melhorar documentação

## 📋 Antes de começar

### Verifique antes de fazer PR

- [ ] Leu o [README.md](README.md)
- [ ] Verificou [Issues existentes](../../issues)
- [ ] Tem o `.git configuration` correto
- [ ] Todos os testes passam localmente

## 🔄 Processo de Contribuição

### 1. Fork & Clone

```bash
# Fork no GitHub (clique em "Fork" no repositório)
git clone https://github.com/SEU_USUARIO/BarberBilling.git
cd BarberBilling
git remote add upstream https://github.com/REPO_ORIGINAL/BarberBilling.git
```

### 2. Crie uma Branch

```bash
# Atualize a main
git checkout main
git fetch upstream
git merge upstream/main

# Crie sua branch
git checkout -b feature/descricao-da-feature
# ou
git checkout -b fix/descricao-do-bug
# ou
git checkout -b docs/descricao-documentacao
```

### Nomenclatura de Branches

- `feature/` - Para novas funcionalidades
- `fix/` - Para correção de bugs
- `docs/` - Para melhorias de documentação
- `refactor/` - Para refatoração de código
- `test/` - Para melhorias em testes

### 3. Faça suas mudanças

```bash
# Edite os arquivos
# Teste suas mudanças
dotnet test

# Commit com mensagem descritiva
git commit -m "feat: descrição clara da mudança"
```

### Padrão de Commit (Conventional Commits)

```
<tipo>[escopo]: <assunto>

<corpo>

<rodapé>
```

**Tipos:**

- `feat:` - Nova funcionalidade
- `fix:` - Correção de bug
- `docs:` - Documentação
- `style:` - Formatação (sem lógica)
- `refactor:` - Refatoração de código
- `perf:` - Melhorias de performance
- `test:` - Testes
- `chore:` - Tarefas de manutenção

**Exemplos:**

```bash
git commit -m "feat(auth): adicionar validação de issuer e audience no JWT"
git commit -m "fix(billing): corrigir cálculo de total em PDF"
git commit -m "docs(readme): atualizar instruções de instalação"
```

### 4. Push e Pull Request

```bash
# Push para seu fork
git push origin feature/descricao-da-feature

# Abra um PR no GitHub
```

### Template de PR

```markdown
## 📝 Descrição

Breve descrição da mudança

## 🎯 Tipo de Mudança

- [ ] Bug fix
- [ ] Nova feature
- [ ] Breaking change
- [ ] Documentação

## 🔗 Issues Relacionadas

Fixes #123

## ✅ Checklist

- [ ] Código segue o padrão do projeto
- [ ] Atualizei a documentação
- [ ] Adicionei testes
- [ ] Todos os testes passam

## 📸 Screenshots (se aplicável)
```

## 💻 Padrões de Código

### C# & .NET

```csharp
// ✅ BOM
public class UsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly ILogger<UsuarioService> _logger;

    public UsuarioService(
        IUsuarioRepository repository,
        ILogger<UsuarioService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter usuário");
            throw;
        }
    }
}

// ❌ RUIM
public class UsuarioService
{
    public UsuarioService() { }

    public Usuario GetUser(Guid id)
    {
        // Sem injeção de dependência
        // Sem tratamento de erro
        return new UsuarioRepository().GetById(id);
    }
}
```

### Princípios SOLID

- **S**ingle Responsibility - Uma classe, uma razão
- **O**pen/Closed - Aberto para extensão, fechado para modificação
- **L**iskov Substitution - Subtypes devem ser substituíveis
- **I**nterface Segregation - Interfaces específicas
- **D**ependency Inversion - Depender de abstrações

### Naming Conventions

| Tipo           | Convenção    | Exemplo              |
| -------------- | ------------ | -------------------- |
| Classes        | PascalCase   | `UsuarioService`     |
| Interfaces     | IPascalCase  | `IUsuarioRepository` |
| Métodos        | PascalCase   | `ObterPorIdAsync()`  |
| Variáveis      | camelCase    | `usuarioId`          |
| Constantes     | PascalCase   | `MaxPasswordLength`  |
| Private fields | \_camelCase  | `_repository`        |
| Async          | Async suffix | `ObterAsync()`       |

### Estrutura de Pastas

```
src/
└── BarberBilling.Application/
    ├── UseCases/
    │   └── Usuarios/
    │       ├── Registrar/
    │       │   ├── RegistrarUsuarioUseCase.cs
    │       │   └── IRegistrarUsuarioUseCase.cs
    │       └── Obter/
    │           ├── ObterUsuarioUseCase.cs
    │           └── IObterUsuarioUseCase.cs
    ├── Mappings/
    │   └── UsuarioMapping.cs
    └── Validators/
        └── UsuarioValidator.cs
```

## ✅ Testes

### Escrever Testes

```csharp
[Fact]
public async Task RegistrarUsuario_ComDadosValidos_DeveRetornarSucesso()
{
    // Arrange
    var request = new RequestRegistroUsuario
    {
        Nome = "João Silva",
        Email = "joao@teste.com",
        Senha = "Senha@123"
    };
    var useCase = new RegistrarUsuarioUseCase(...);

    // Act
    var resultado = await useCase.Execute(request);

    // Assert
    resultado.Should().NotBeNull();
    resultado.Nome.Should().Be("João Silva");
}

[Fact]
public async Task RegistrarUsuario_ComEmailInvalido_DeveLancarExcecao()
{
    // Arrange
    var request = new RequestRegistroUsuario { Email = "invalido" };

    // Act & Assert
    await Assert.ThrowsAsync<ValidationException>(() =>
        useCase.Execute(request));
}
```

### Executar Testes

```bash
# Todos os testes
dotnet test

# Projeto específico
dotnet test tests/Validator.Tests/

# Teste específico
dotnet test --filter "RegistrarUsuario"

# Com cobertura
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## 🔐 Segurança

### Não commitir

❌ Senhas  
❌ API Keys  
❌ Tokens  
❌ Informações sensíveis

### Usar sempre

✅ User Secrets em desenvolvimento  
✅ Environment variables em produção  
✅ Secret management (Azure Key Vault, etc)

## 📝 Documentação

### Comentários de Código

```csharp
/// <summary>
/// Registra um novo usuário no sistema
/// </summary>
/// <param name="request">Dados do usuário a registrar</param>
/// <returns>Resposta com dados do usuário criado</returns>
/// <exception cref="EmailJaExisteException">
/// Lançada quando email já está registrado
/// </exception>
public async Task<ResponseRegistroUsuario> Execute(
    RequestRegistroUsuario request)
{
    // Implementação
}
```

### README.md

Atualize o README se:

- Adicionar nova feature
- Mudar padrão de configuração
- Documentar novo endpoint
- Adicionar dependências

## 🔍 Code Review

### O que esperar

- Revisão de código
- Testes de funcionalidade
- Verificação de security
- Feedback construtivo

### Como responder

- Agradeça o feedback
- Faça ajustes se necesário
- Peça esclarecimentos se confuso
- Request review novamente quando pronto

## 🚀 Processo de Merge

1. Todos os testes devem passar
2. Pelo menos um maintainer deve revisar
3. Sem conflitos de merge
4. Documentação atualizada
5. Branch apagada após merge

## 📚 Recursos

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [C# Coding Guidelines](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)

## ❓ Dúvidas?

- 💬 Abra uma [Discussion](../../discussions)
- 🐛 [Reporte um bug](../../issues/new?template=bug_report.md)
- 💡 [Sugira uma feature](../../issues/new?template=feature_request.md)

## 📜 Código de Conduta

Por favor, leia nosso [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) antes de participar.

---

Obrigado por contribuir! 🎉
