# 📚 BookReview API

> Uma API RESTful robusta para gerenciamento de livros e revisões, implementada com .NET 10, arquitetura em camadas e documentação interativa via Scalar.

![.NET](https://img.shields.io/badge/.NET%2010-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Scalar](https://img.shields.io/badge/Scalar-Api%20Reference-black?style=for-the-badge)
![EF Core](https://img.shields.io/badge/EF%20Core-Entity%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)

## 📖 Sobre o Projeto

O **BookReview** é um backend completo que simula uma plataforma de crítica literária. O sistema foi desenhado para gerenciar relacionamentos complexos entre autores, livros e categorias, além de permitir que revisores (reviewers) avaliem as obras.

O projeto adota práticas modernas de engenharia de software, como o uso de **DTOs (Data Transfer Objects)** para limpar os contratos da API e o **Repository Pattern** para abstrair o acesso a dados.

## 🚀 Tecnologias e Arquitetura

* **[.NET 10](https://dotnet.microsoft.com/)**: Framework de alta performance.
* **[SQL Server](https://www.microsoft.com/sql-server)**: Banco de dados relacional.
* **[Entity Framework Core](https://learn.microsoft.com/ef/core/)**: ORM avançado com mapeamento de relacionamentos N:N.
* **[Scalar](https://scalar.com/)**: Documentação de API moderna e interativa (substituto ao Swagger).

### 🏗️ Destaques da Arquitetura
* **Repository Pattern**: Desacoplamento completo entre a lógica de negócios e o banco de dados.
* **DTOs (Data Transfer Objects)**: Uso estrito de DTOs para evitar exposição direta das entidades de domínio (`Models`).
* **Modelagem Relacional Complexa**: Implementação de tabelas de junção (`BookAuthor`, `BookCategory`) para suportar relacionamentos Muitos-para-Muitos.

## ✨ Funcionalidades

A API oferece endpoints organizados para gerenciar:

* 📚 **Books**: Cadastro principal de obras.
* ✍️ **Authors**: Gestão de autores (com suporte a múltiplos livros).
* 🏷️ **Categories**: Classificação de gêneros (ação, drama, técnico, etc.).
* 🌍 **Countries**: Origem dos autores e publicações.
* ⭐ **Reviews**: Avaliações detalhadas com notas e comentários.
* 👤 **Reviewers**: Perfil dos usuários que realizam as críticas.

## 🛠️ Como Executar

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download)
* SQL Server

### Passo a Passo (Visual Studio)

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/gabmiranda7/BookReview.git
    ```
    *Ou use a opção "Clonar Repositório" na tela inicial do Visual Studio.*

2.  **Configure o Banco de Dados:**
    Ajuste a `ConnectionStrings` no arquivo `appsettings.json` para o seu servidor local.

3.  **Execute as Migrations:**
    Abra o **Package Manager Console** (*Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes*) e execute:
    ```powershell
    Update-Database
    ```

4.  **Inicie a Aplicação:**
    Pressione **F5** ou clique no botão **Inciar/Play** na barra de ferramentas superior para rodar o projeto.

## 📄 Documentação (Scalar)

Acesse a interface interativa para testar os endpoints em tempo real:

🔗 **URL Local:** `http://localhost:PORT/scalar/v1`

## 📂 Estrutura do Projeto

A organização das pastas segue o padrão de separação de responsabilidades:

```text
BookReview/
├── Controllers/       # Endpoints da API (Books, Authors, Reviews, etc.)
├── Data/              # Contexto do Banco de Dados (AppDbContext)
├── DTOs/              # Contratos de dados para entrada e saída da API
├── Interfaces/        # Contratos dos Repositórios (IAuthorRepository, etc.)
├── Migrations/        # Histórico de versões do banco de dados
├── Models/            # Entidades de Domínio e Tabelas de Junção (BookAuthor, etc.)
├── Repository/        # Implementação do acesso a dados
└── Program.cs         # Injeção de dependência e configuração do pipeline
