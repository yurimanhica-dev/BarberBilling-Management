# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

O formato é baseado em [Keep a Changelog](https://keepachangelog.com/pt-BR/),
e este projeto adere a [Semantic Versioning](https://semver.org/lang/pt-BR/).

## [Unreleased]

### Adicionado

- Suporte a Docker Compose
- Autenticação com OAuth2 (planejado)
- Sistema de notificações por email
- Dashboard de analytics

### Mudado

- Melhorias de performance no relatório PDF
- Refatoração de validações

### Segurança

- Atualização de dependências de segurança

---

## [1.0.0] - 2026-04-06

### Adicionado

- ✅ **Autenticação JWT** com Argon2id
  - Login com email e senha
  - Registro de clientes
  - Refresh token
  - Logout/revogação de token
  - Token versioning para segurança

- ✅ **Autorização Role-Based (RBAC)**
  - Sistema de roles (Admin, Barbeiro, Cliente)
  - Permissões granulares
  - Policy-based authorization

- ✅ **Gerenciamento de Usuários**
  - Criar usuários (Admin)
  - Registrar como cliente
  - Gestão de roles e permissões

- ✅ **Gestão de Serviços**
  - CRUD completo
  - Categorização
  - Preços flexíveis

- ✅ **Sistema de Faturação**
  - Criar faturação com múltiplos serviços
  - Snapshot de preços
  - Atualizar status
  - Deletar faturação
  - Cálculo automático de totais
  - Filtros avançados

- ✅ **Relatórios**
  - Geração de PDF com PdfSharp
  - Histórico completo
  - Detalhes de serviços e preços

- ✅ **Categorias**
  - CRUD de categorias
  - Associação com serviços

- ✅ **API REST**
  - 7 controllers completos
  - Documentação interativa com Scalar
  - Validação fluente
  - Tratamento centralizado de erros

- ✅ **Segurança**
  - CORS configurável
  - HTTPS redirecionamento
  - User Secrets para desenvolvimento
  - Validação de senha forte
  - Transporte seguro

- ✅ **Testes**
  - Testes de validação
  - Testes unitários
  - Cobertura de casos principais

- ✅ **Documentação**
  - README completo
  - Guia de User Secrets
  - Exemplos de API
  - Setup instructions
  - Arquitetura documentada

- ✅ **DevOps**
  - GitHub Actions CI/CD
  - Database migrations
  - Health checks

### Estrutura & Arquitetura

- Clean Architecture em 6 camadas
- Domain-Driven Design (DDD)
- SOLID Principles
- Repository Pattern
- Dependency Injection
- Entity Framework Core

### Dependências Principais

- .NET 10.0
- Entity Framework Core 10.0
- JWT Bearer 10.0
- Scalar.AspNetCore 2.13.3
- FluentValidation
- AutoMapper
- PdfSharp

### Configuração

- appsettings.json e Development.json
- User Secrets suportado
- CORS configurável
- Localization (PT-BR, EN)

### Database

- PostgreSQL
- Migrations automáticas
- Seeding de dados

## [0.1.0] - 2026-01-01

### Adicionado

- Projeto inicial criado
- Setup de estrutura base

---

## Guia para Atualizar Este Arquivo

### Ao Adicionar Mudanças:

1. Vá para a seção [Unreleased]
2. Categorize a mudança:
   - **Adicionado** para novas funcionalidades
   - **Mudado** para mudanças em funcionalidades existentes
   - **Depreciado** para funcionalidades que serão removidas
   - **Removido** para funcionalidades removidas
   - **Consertado** para correções de bugs
   - **Segurança** para vulnerabilidades

3. Commit com mensagem: `docs: atualizar CHANGELOG`

### Ao Fazer Release:

1. Renomeie [Unreleased] para [X.Y.Z] - YYYY-MM-DD
2. Adicione link de comparação no final
3. Crie novo [Unreleased]

### Exemplo de Link:

```
[Unreleased]: https://github.com/usuario/BarberBilling/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/usuario/BarberBilling/releases/tag/v1.0.0
[0.1.0]: https://github.com/usuario/BarberBilling/releases/tag/v0.1.0
```

---

**Status**: Em desenvolvimento ativo 🚀
