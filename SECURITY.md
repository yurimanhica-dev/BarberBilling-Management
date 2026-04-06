# Política de Segurança

## Reportando Vulnerabilidades

Se você descobrir uma vulnerabilidade de segurança no BarberBilling, por favor **não abra uma Issue pública**. Em vez disso, por favor reporte responsavelmente através de:

### Contato Seguro

1. **GitHub Security Advisories** (Preferido)
   - Vá para [Security](../../security/advisories)
   - Clique em "Report a vulnerability"
   - Descreva o problema em detalhes

2. **Email**
   - Envie para: `security@seu-dominio.com` (será adicionado quando o projeto estiver público)
   - Assunto: `[SECURITY] BarberBilling - Descrição breve`
   - Inclua detalhes de reprodução

### O que incluir no relatório

```
Título: Uma linha descritiva da vulnerabilidade

Descrição:
- Tipo de vulnerabilidade (Ex: SQL Injection, XSS, CSRF, etc)
- Localização (Arquivo, linha, function)
- Severidade (Crítica, Alta, Média, Baixa)

Passos para Reproduzir:
1. ...
2. ...
3. ...

Impacto:
- Quem é afetado
- Qual é o risco
- Cenário de ataque

Sugestão de Fix (se houver):
```

## Processo de Resposta

1. **Reconhecimento** (24-48h)
   - Receberá confirmação de recebimento
   - Será atribuído um ID de rastreamento

2. **Investigação** (3-7 dias)
   - Analisamos a vulnerabilidade
   - Desenvolvemos um fix
   - Criamos testes

3. **Disclosure** (variável)
   - Publicamos o fix
   - Divulgamos responsavelmente a comunidade
   - Você recebe crédito (se desejar)

## Práticas de Segurança do Projeto

### Autenticação & Tokens

- ✅ JWT com Argon2id
- ✅ Validação de Issuer e Audience
- ✅ Refresh token com revogação
- ✅ Token versioning
- ✅ Expiração configurável

### Senhas

- ✅ Hashing com Argon2id + pepper
- ✅ Validação de força (8-15 chars)
- ✅ Requer: maiúscula, minúscula, número, símbolo
- ✅ Sem armazenamento em plain text

### Entrada de Dados

- ✅ Validação fluente em todos endpoints
- ✅ Sanitização de inputs
- ✅ Proteção contra SQL Injection (EF Core)
- ✅ Prevenção de XXE

### API

- ✅ CORS restritivo
- ✅ HTTPS obrigatório
- ✅ Rate limiting (planejado)
- ✅ CSRF protection

### Dependências

- ✅ Mantidas atualizadas
- ✅ Scanners de vulnerabilidade
- ✅ Auditoria regular

### Dados Sensíveis

- ✅ Secrets não em versionamento
- ✅ User Secrets em development
- ✅ Environment variables em produção
- ✅ Sem logs de dados sensíveis

### Erros & Logs

- ✅ Sem exposição de stack trace em produção
- ✅ Mensagens de erro genéricas ao usuário
- ✅ Logs detalhados internos
- ✅ Auditoria em produção

## Checklist de Segurança para Contribuidores

Ao contribuir, verifique:

- [ ] Não comitei secrets
- [ ] Validei todos os inputs
- [ ] Usei prepared statements (EF Core)
- [ ] Não executei código dinâmico
- [ ] Tratei erros sem expor detalhes
- [ ] Segui OWASP Top 10
- [ ] Adicionei testes de segurança
- [ ] Documentei considerações de segurança

## Referências de Segurança

### OWASP

- [OWASP Top 10](https://owasp.org/Top10/)
- [Cheat Sheets](https://cheatsheetseries.owasp.org/)
- [Testing Guide](https://owasp.org/www-project-web-security-testing-guide/)

### .NET Security

- [Microsoft Security Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/security/)
- [ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/)
- [CWE List](https://cwe.mitre.org/)

### Padrões & Standards

- [RFC 7231 - HTTP/1.1](https://tools.ietf.org/html/rfc7231)
- [RFC 7235 - Authentication](https://tools.ietf.org/html/rfc7235)
- [RFC 8174 - JWT](https://tools.ietf.org/html/rfc8174)

## Divulgação Responsável

Acreditamos em divulgação responsável. Isso significa:

1. ✅ **Reporte privadamente** vulnerabilidades
2. ✅ **Dê tempo** para corrigir (30-90 dias)
3. ✅ **Trabalhe conosco** no fix
4. ✅ **Receba crédito** por sua contribuição
5. ✅ **Defina embargo** se necessário

## Histórico de Vulnerabilidades

Nenhuma vulnerabilidade reportada até o momento (v1.0.0)

## Agradecimentos

Obrigado aos security researchers que reportam vulnerabilidades responsavelmente!

Inclua seu nome aqui quando reportar:

- [Adicionar reportadores aqui]

---

**Versão**: 1.0  
**Última Atualização**: 2026-04-06  
**Status**: Ativo ✅
