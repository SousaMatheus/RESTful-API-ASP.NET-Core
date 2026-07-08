# RESTful API — ASP.NET Core

API RESTful em **ASP.NET Core** aplicando boas práticas de design de APIs e **arquitetura em camadas**, com separação entre apresentação, regras de negócio e acesso a dados.

## 🎯 O que demonstra

- Design de **endpoints RESTful** — verbos HTTP, status codes e rotas semânticas
- **Arquitetura em camadas** com separação de responsabilidades
- **Regras de negócio isoladas** da camada de apresentação — controllers finos
- **Acesso a dados desacoplado** via camada dedicada (Repository Pattern)
- Validação e notificação de erros retornando respostas HTTP adequadas

## 🏗️ Estrutura da solução

```
API.MSDev.sln
└── src/
    ├── API.MSDev/       # Camada de apresentação — controllers, DTOs, configuração
    ├── DevMS.Business/  # Regras de negócio — services, validações, interfaces
    └── DevMS.Data/      # Acesso a dados — DbContext, repositórios, mapeamentos
```

## 🚀 Como executar

Pré-requisito: [.NET SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://github.com/SousaMatheus/RESTful-API-ASP.NET-Core.git
cd RESTful-API-ASP.NET-Core
dotnet run --project src/API.MSDev
```

Acesse o Swagger em `https://localhost:{porta}/swagger` para explorar os endpoints.
