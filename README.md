# 💈 BarberBilling - Sistema de Faturação para Barbearias

<div align="center">

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-Latest-239120?style=for-the-badge&logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Visual Studio Code](https://img.shields.io/badge/VS%20Code-1.95-007ACC?style=for-the-badge&logo=visualstudiocode)](https://code.visualstudio.com/)
[![Scalar API](https://img.shields.io/badge/Scalar%20API-2.13.3-1A202C?style=for-the-badge&logo=swagger)](https://scalar.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Latest-336791?style=for-the-badge&logo=postgresql)](https://www.postgresql.org/)
[![PdfSharp](https://img.shields.io/badge/PdfSharp-Reports-FF6B00?style=for-the-badge&logo=adobeacrobatreader)](https://github.com/ststeiger/PdfSharpCore)
[![Windows](https://img.shields.io/badge/Windows-11-0078D4?style=for-the-badge&logo=windows)](https://www.microsoft.com/pt-br/)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=for-the-badge)]()

**Uma solução moderna e segura para gerenciar faturação, serviços e relatórios de barbearias**

[Documentação](#-documentação) • [Getting Started](#-getting-started) • [Arquitetura](#-arquitetura) • [Contribuindo](#-contribuindo)

</div>

---

## 📋 Sumário

- [Sobre o Projeto](#-sobre-o-projeto)
- [✨ Funcionalidades Principais](#-funcionalidades-principais)
- [📸 Screenshots e Relatórios](#-screenshots-e-relatórios)
- [🛠️ Stack Tecnológico](#️-stack-tecnológico)
- [📋 Requisitos](#-requisitos)
- [🚀 Getting Started](#-getting-started)
- [📁 Estrutura do Projeto](#-estrutura-do-projeto)
- [🏗️ Arquitetura](#️-arquitetura)
- [🔐 Segurança](#-segurança)
- [📚 Documentação da API](#-documentação-da-api)
- [✅ Testes](#-testes)
- [🐳 Docker](#-docker)
- [📝 Configuração](#-configuração)
- [🤝 Contribuindo](#-contribuindo)
- [📄 Licença](#-licença)

---

## 📖 Sobre o Projeto

**BarberBilling** é um sistema completo de gerenciamento e faturação desenvolvido especificamente para barbearias. Oferece uma solução robusta, segura e escalável para:

- ✅ Autenticação e autorização baseada em roles e permissões
- ✅ Gerenciamento de usuários (Barbeiros, Clientes, Administradores)
- ✅ Catálogo de serviços com categorias
- ✅ Criação e atualização de faturação com snapshot de preços
- ✅ Geração de relatórios em PDF com cálculos automáticos
- ✅ Suporte a múltiplos métodos de pagamento
- ✅ API RESTful com documentação interativa via Scalar

O projeto segue **arquitetura em camadas** com separação clara de responsabilidades, utiliza **padrões SOLID** e implementa **boas práticas de segurança**.

---

## ✨ Funcionalidades Principais

### 🔐 Autenticação & Autorização

- **JWT Bearer Tokens** com validação de Issuer e Audience
- **Refresh token** com revogação automática
- **Role-based Access Control (RBAC)**
- **Permission-based policies** granulares
- Criptografia de senhas com **Argon2id**
- Validação de força de senha (8-15 caracteres, maiúscula, minúscula, número, símbolo)

### 👥 Gerenciamento de Usuários

- Registro de clientes (auto-registro)
- Criar usuários (Admin only)
- Gestão de roles e permissões
- Token versioning para invalidação segura
- Suporte a múltiplos papéis (Barbeiro, Cliente, Admin)

### 💼 Gerenciamento de Serviços

- **CRUD completo** de serviços
- Organização por categorias
- Preços flexíveis
- Snapshot de preços na faturação

### 📊 Faturação & Relatórios

- Criar faturação com múltiplos serviços
- Atualizar status de faturação
- Cálculo automático de totais
- **Gerar relatórios em PDF** com histórico completo
- Filtros avançados por data, cliente, barbeiro, status
- Suporte a métodos de pagamento variados

### 📈 Recursos Adicionais

- **Localização** (PT-BR, EN)
- **CORS configurável** por ambiente
- **Validação fluente** de dados
- **Tratamento centralizado de exceções**
- **Health checks** e métricas

---

## 📸 Screenshots e Relatórios

### Documentação Interativa (Scalar)

A API possui documentação completa e interativa através do **Scalar API Reference**:

```
https://localhost:7001/scalar/v1
```

> 📸 _[Adicione screenshots da API aqui]_

### Exemplo de Relatório em PDF

O sistema gera relatórios profissionais com:

- Detalhes do barbeiro e cliente
- Lista de serviços com preços
- Total da faturação
- Data e forma de pagamento

> 📸 _[Adicione screenshots de relatórios aqui]_

### Dashboard de Faturação

Visualize e gerencie todas as faturações:

- Filtros por data, cliente, status
- Ações rápidas (editar, deletar, gerar PDF)
- Estatísticas de receita

> 📸 _[Adicione screenshots do dashboard aqui]_

---

## 🛠️ Stack Tecnológico

### Backend

| Tecnologia                | Versão | Uso            |
| ------------------------- | ------ | -------------- |
| **.NET**                  | 10.0   | Runtime        |
| **C#**                    | Latest | Linguagem      |
| **ASP.NET Core**          | 10.0   | Framework Web  |
| **Entity Framework Core** | 10.0   | ORM            |
| **PostgreSQL**            | Latest | Banco de Dados |
| **JWT Bearer**            | 10.0   | Autenticação   |

### Bibliotecas & Packages

| Pacote                | Versão | Uso              |
| --------------------- | ------ | ---------------- |
| **Scalar.AspNetCore** | 2.13.3 | Documentação API |
| **PdfSharpCore**      | -      | Geração de PDFs  |
| **Argon2id**          | Latest | Hash de Senhas   |
| **FluentValidation**  | Latest | Validação        |
| **AutoMapper**        | Latest | Mapeamento       |

### Ferramentas

| Ferramenta             | Versão | Uso                |
| ---------------------- | ------ | ------------------ |
| **Visual Studio Code** | 1.95+  | IDE                |
| **Docker**             | Latest | Conteinerização    |
| **Git**                | Latest | Controle de Versão |
| **Scalar**             | 2.13.3 | Documentação       |

---

## 📋 Requisitos

### Obrigatórios

- **[.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)** ou superior
- **[PostgreSQL](https://www.postgresql.org/download/)** 13+
- **[Git](https://git-scm.com/download/win)** para Windows, Mac ou Linux
- **[Visual Studio Code](https://code.visualstudio.com/)** (recomendado)

### Recomendado

- **Docker** & **Docker Compose** (para ambiente conteinerizado)
- **Postman** ou **Insomnia** (para testes de API)
- **pgAdmin** (gerenciamento de PostgreSQL)

### .NET SDK - Links de Download

| Versão             | Link                                                          |
| ------------------ | ------------------------------------------------------------- |
| **.NET 10.0**      | [Download](https://dotnet.microsoft.com/download/dotnet/10.0) |
| **.NET 9.0** (LTS) | [Download](https://dotnet.microsoft.com/download/dotnet/9.0)  |
| **.NET Latest**    | [Download](https://dotnet.microsoft.com/download)             |

**Verificar versão instalada:**

```bash
dotnet --version
```

---

## 🚀 Getting Started

### 1️⃣ Clone o Repositório

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/BarberBilling.git

# Entre no diretório
cd BarberBilling

# Verifique a branch
git status
```

### 2️⃣ Configure as Variáveis de Ambiente

#### Opção A: User Secrets (Recomendado para Desenvolvimento)

```bash
# Acesse a pasta da API
cd src/BarberBilling.Api

# Inicialize User Secrets (primeira vez)
dotnet user-secrets init

# Configure os secrets
dotnet user-secrets set "Settings:Jwt:Key" "seu_jwt_key_minimo_32_caracteres_aqui_para_desenvolvimento"
dotnet user-secrets set "Settings:Jwt:ExpirationInMinutes" "60"
dotnet user-secrets set "Settings:Jwt:RefreshTokenExpirationInDays" "7"
dotnet user-secrets set "Security:Pepper" "seu_pepper_desenvolvimento_minimo_20_caracteres"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=BarberBilling;User Id=postgres;Password=seu_password;"

# Listar todos os secrets
dotnet user-secrets list
```

#### Opção B: Arquivo de Configuração Local

```bash
# Copie o arquivo de exemplo
cp src/BarberBilling.Api/appsettings.secrets.example.json src/BarberBilling.Api/appsettings.secrets.json

# Edite com suas informações
code src/BarberBilling.Api/appsettings.secrets.json
```

**Arquivo de exemplo (`appsettings.Development.json`):**

```json
{
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:3000",
      "http://localhost:5173",
      "http://localhost:4200"
    ]
  },
  "Settings": {
    "Jwt": {
      "ValidIssuer": "BarberBilling",
      "ValidAudience": "BarberBillingApp"
    }
  }
}
```

### 3️⃣ Execute e Teste a Aplicação

```bash
# Restaure dependências
dotnet restore

# Aplique migrações do banco de dados
cd src/BarberBilling.Api
dotnet ef database update

# Execute a aplicação
dotnet run

# ✅ A API estará disponível em:
# → https://localhost:7001
# → Documentação: https://localhost:7001/scalar/v1
```

#### Executar Testes

```bash
# Teste de validadores
dotnet test tests/Validator.Tests/Validators.Tests.csproj --verbosity normal

# Todos os testes
dotnet test

# Com cobertura detalhada
dotnet test /p:CollectCoverage=true
```

---

## 📁 Estrutura do Projeto

```
BarberBilling/
├── 📄 README.md                           # Este arquivo
├── 📄 BarberBilling.slnx                  # Solução
├── 📄 USER_SECRETS_SETUP.md              # Guia de User Secrets
├── .github/
│   └── workflows/
│       └── ci-cd.yml                      # Pipeline CI/CD
├── src/
│   ├── BarberBilling.Api/                 # 🌐 Camada de Apresentação
│   │   ├── appsettings.json               # Configurações
│   │   ├── appsettings.Development.json   # Dev only
│   │   ├── appsettings.secrets.example.json # Template secrets
│   │   ├── Program.cs                     # Ponto de entrada
│   │   ├── Controller/                    # Endpoints REST
│   │   │   ├── AuthenticationController.cs
│   │   │   ├── BillingsController.cs
│   │   │   ├── ServicesController.cs
│   │   │   ├── UsersController.cs
│   │   │   └── ...
│   │   ├── Extensions/                    # Extensões de DI
│   │   ├── Middleware/                    # Middlewares customizados
│   │   ├── Filters/                       # Filtros (Exception, etc)
│   │   └── Security/                      # Handlers de autorização
│   │
│   ├── BarberBilling.Application/         # 📦 Camada de Aplicação
│   │   ├── UseCases/                      # Use cases por feature
│   │   │   ├── Authentication/
│   │   │   ├── Authorization/
│   │   │   ├── Billings/
│   │   │   ├── Services/
│   │   │   ├── Users/
│   │   │   └── Categories/
│   │   ├── Mappings/                      # AutoMapper profiles
│   │   ├── Validators/                    # Validação fluente
│   │   └── DependencyInjectionExtension.cs
│   │
│   ├── BarberBilling.Communication/       # 📨 DTOs & Modelos
│   │   ├── Requests/                      # Modelos de entrada
│   │   ├── Responses/                     # Modelos de saída
│   │   └── Enums/                         # Enumerações
│   │
│   ├── BarberBilling.Domain/              # 🎯 Camada de Domínio
│   │   ├── Entities/                      # Entidades de negócio
│   │   ├── Repositories/                  # Interfaces de repositório
│   │   ├── Enums/                         # Enums de domínio
│   │   └── Security/                      # Interfaces de segurança
│   │
│   ├── BarberBilling.Infrastructure/      # 🔧 Camada de Infraestrutura
│   │   ├── Persistence/                   # DbContext, Repositórios
│   │   │   ├── Context/
│   │   │   └── Repositories/
│   │   ├── Migrations/                    # EF Core Migrations
│   │   ├── Security/                      # Implementações de segurança
│   │   └── DependencyInjectionExtension.cs
│   │
│   └── BarberBilling.Exceptions/          # ⚠️ Exceções customizadas
│       ├── CustomExceptions/
│       └── BarberBillingException.cs
│
├── tests/
│   ├── BarberBilling.Tests/               # Testes unitários
│   └── Validator.Tests/                   # Testes de validação
│
└── .gitignore                             # Arquivo de exclusão Git

```

---

## 🏗️ Arquitetura

### Padrão em Camadas

```
┌─────────────────────────────────────────┐
│    Presentation Layer (API)             │ Controllers, Middleware, Filters
│    BarberBilling.Api                    │
└─────────────────────────────────────────┘
                ↓
┌─────────────────────────────────────────┐
│    Application Layer                    │ Use Cases, Mappings, Validators
│    BarberBilling.Application            │
├─────────────────────────────────────────┤
│    Communication Layer (DTOs)           │ Requests, Responses, Enums
│    BarberBilling.Communication          │
└─────────────────────────────────────────┘
                ↓
┌─────────────────────────────────────────┐
│    Domain Layer                         │ Entities, Business Logic
│    BarberBilling.Domain                 │ Repository Interfaces
└─────────────────────────────────────────┘
                ↓
┌─────────────────────────────────────────┐
│    Infrastructure Layer                 │ EF Core, Repositories, Security
│    BarberBilling.Infrastructure         │ Migrations, Database
│    BarberBilling.Exceptions             │ Custom Exceptions
└─────────────────────────────────────────┘
```

### Benefícios

✅ **Separação de Responsabilidades** - Cada camada tem um propósito específico  
✅ **Testabilidade** - Fácil mockar dependências com interfaces  
✅ **Manutenibilidade** - Código organizado e previsível  
✅ **Escalabilidade** - Fácil adicionar novos features

---

## 🔐 Segurança

### Implementações de Segurança

| Feature                | Implementação                            |
| ---------------------- | ---------------------------------------- |
| **Autenticação**       | JWT Bearer com Argon2id                  |
| **Validação JWT**      | Issuer, Audience, Signature              |
| **Hashssing de Senha** | Argon2id com pepper                      |
| **Token Versioning**   | Revogação automática                     |
| **CORS**               | Configurável por ambiente                |
| **HTTPS**              | Redirecionamento automático              |
| **User Secrets**       | Sensíveis não em versionamento           |
| **Validação de Input** | FluentValidation                         |
| **Tratamento de Erro** | Sem exposição de stack trace em produção |

### Senhas Fortes

- Comprimento: 8-15 caracteres
- Requer: Maiúscula, Minúscula, Número, Símbolo
- Sem espaços

---

## 📚 Documentação da API

### Acessar Documentação Interativa

A documentação completa da API está disponível através do **Scalar** em tempo de execução:

```
https://localhost:7001/scalar/v1
```

### Recursos da Documentação

- 📖 **Endpoints interativos** - Teste diretamente na documentação
- 🔍 **Modelos detalhados** - Veja óbrigações, tipos e validações
- 🔐 **Suporte a autenticação** - Teste endpoints protegidos
- 📋 **Exemplos de requisição/resposta**
- 🏷️ **Schemas OpenAPI/Swagger**

### Exemplo de Uso - Autenticação

**1. Registrar um cliente:**

```bash
POST /api/v1/authentication/register-cliente
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@exemplo.com",
  "password": "Senha@123"
}

Response:
{
  "name": "João Silva",
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

**2. Login:**

```bash
POST /api/v1/authentication/login
Content-Type: application/json

{
  "email": "joao@exemplo.com",
  "password": "Senha@123"
}

Response:
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "eyJhbGciOiJIUzI1NiIs..."
}
```

**3. Usar o token:**

```bash
GET /api/v1/billings
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

---

## ✅ Testes

### Executar Testes

```bash
# Todos os testes
dotnet test

# Teste específico (validadores)
dotnet test tests/Validator.Tests/

# Com verbosidade detalhada
dotnet test --verbosity detailed

# Com cobertura de código
dotnet test /p:CollectCoverage=true
```

### Estrutura de Testes

```
tests/
├── BarberBilling.Tests/
│   └── Testes integracionais
└── Validator.Tests/
    └── Testes de validação (usuários, senhas, etc)
```

### Testes Implementados

- ✅ Validação de força de senha
- ✅ Validação de email
- ✅ Criação de usuários
- ✅ Autenticação e autorização
- ✅ Faturação e cálculos
- ✅ Geração de PDF

---

## 🐳 Docker

### Usando Docker Compose

```bash
# Subir toda a stack (API + PostgreSQL)
docker-compose up -d

# Logs em tempo real
docker-compose logs -f barberbilling-api

# Parar tudo
docker-compose down

# Ver containers rodando
docker-compose ps
```

### Endpoints

- **API**: http://localhost:7001
- **Documentação**: http://localhost:7001/scalar/v1
- **PostgreSQL**: localhost:5432

---

## 📝 Configuração

### Variáveis de Ambiente

| Variável                               | Descrição              | Exemplo                     |
| -------------------------------------- | ---------------------- | --------------------------- |
| `Settings__Jwt__Key`                   | Chave secreta JWT      | `sua_chave_minimo_32_chars` |
| `Settings__Jwt__ValidIssuer`           | Emissor do JWT         | `BarberBilling`             |
| `Settings__Jwt__ValidAudience`         | Audiência do JWT       | `BarberBillingApp`          |
| `Settings__Jwt__ExpirationInMinutes`   | Expiração do token     | `60`                        |
| `Security__Pepper`                     | Pepper para hashing    | `seu_pepper_aleatorio`      |
| `ConnectionStrings__DefaultConnection` | String de conexão BD   | `Server=...;`               |
| `Cors__AllowedOrigins__0`              | URL frontend permitida | `http://localhost:3000`     |

### Arquivos de Configuração

**`appsettings.json`** - Configurações padrão (NÃO colocar secrets)

**`appsettings.Development.json`** - Configurações específicas de dev

**User Secrets** - Para valores sensíveis em desenvolvimento (recomendado)

Mais detalhes em [USER_SECRETS_SETUP.md](USER_SECRETS_SETUP.md)

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para começar:

1. **Faça um Fork** do projeto
2. **Crie uma branch** para sua feature (`git checkout -b feature/NovaFuncionalidade`)
3. **Commit suas mudanças** (`git commit -m 'Add NovaFuncionalidade'`)
4. **Push para a branch** (`git push origin feature/NovaFuncionalidade`)
5. **Abra um Pull Request**

### Diretrizes

- Siga o padrão de código existente
- Adicione testes para novas features
- Atualize a documentação
- Siga os padrões SOLID

---

## 📊 Status do Projeto

| Aspecto               | Status                |
| --------------------- | --------------------- |
| **Autenticação**      | ✅ Completo           |
| **Autorização**       | ✅ Completo           |
| **CRUD de Usuários**  | ✅ Completo           |
| **CRUD de Serviços**  | ✅ Completo           |
| **CRUD de Faturação** | ✅ Completo           |
| **Relatórios PDF**    | ✅ Completo           |
| **Categorias**        | ✅ Completo           |
| **Tests**             | ✅ Completo           |
| **Documentação API**  | ✅ Completo           |
| **Docker**            | 🔄 Em desenvolvimento |
| **CI/CD**             | ✅ GitHub Actions     |

---

## 📞 Suporte

Para dúvidas ou problemas:

1. 📖 Consulte [USER_SECRETS_SETUP.md](USER_SECRETS_SETUP.md)
2. 📚 Acesse a documentação interativa em http://localhost:7001/scalar/v1
3. 🐛 Abra uma [Issue](https://github.com/seu-usuario/BarberBilling/issues)
4. 💬 Discussões na [aba de Discussões](https://github.com/seu-usuario/BarberBilling/discussions)

---

## 📄 Licença

Este projeto está licenciado sob a **Licença MIT** - veja o arquivo [LICENSE](LICENSE) para detalhes.

---

## 🙏 Agradecimentos

- [Rocketseat](https://www.rocketseat.com.br/) - Comunidade incrível de desenvolvimento
- [Microsoft](https://www.microsoft.com/) - .NET Framework
- [PostgreSQL](https://www.postgresql.org/) - Database
- Todos os [contributors](https://github.com/seu-usuario/BarberBilling/graphs/contributors)

---

<div align="center">

**[⬆ Voltar ao topo](#-barberbilling---sistema-de-faturação-para-barbearias)**

Desenvolvido com ❤️ para a comunidade de desenvolvedores .NET

![GitHub followers](https://img.shields.io/github/followers/seu-usuario?style=social)
![GitHub stars](https://img.shields.io/github/stars/seu-usuario/BarberBilling?style=social)

</div>
