# **[BlogExpert] - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **[BlogExpert]**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal é desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.
A aplicação foi dividida em camadas para separar algumas reponsabilidades, principalmente em relação ao repositório de dados e de negócio. As validações são garantidas na camada de negócios.
Para facilitar os testes por quem baixar o projeto foi adicionado um tratamento para a criação do banco de dados junto com uma massa de dados que preenche usuários, autores, posts e comentários.
O usuário admin@be.net é o administrador que possui a role admin na carga inicial. Os demais usuários são comuns e não possuem a role admin.
Apenas usuários com a role admin conseguem manipular todos os dados de forma livre no blog. Os usuários comuns só conseguem manipular os dados por eles inseridos.
Os autores também conseguem manipular todos os dados dos posts que lhe são atribuídos a autoria e os comentários vinculados a esses posts.

### **Autor**
- **Marcelo Santos Menezes**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o blog.
- **API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- src/
  - BlogExpert.Mvc/ - Projeto MVC
  - BlogExpert.Api/ - API RESTful
  - BlogExpert.Dados/ - Modelos de Dados e Configuração do EF Core
  - BlogExpert.Negocio/ - Serviços de negócios
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **Registro de Usuários:** Permite incluir usuários ao blog que são inseridos também como autores para a publicação de posts.
- **Gestão de usuários:** Foi adicionado na aplicação MVC os recursos disponibilizados pelo identity com a tradução para portugês.
- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/marcelosolusist/mba-devexpert-p01-blog.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server nos projetos BlogExpert.Mvc e BlogExpert.Api.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

3. **Executar a Aplicação MVC:**
   - `cd src/BlogExpert.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: http://localhost:5249/

4. **Executar a API:**
   - `cd src/BlogExpert.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5078/swagger/ 
   
5. **Usuários registrados na carga inicial:**
   - admin@be.net (usuário administrador)
   - be@be.net (usuário comum)
   - marcelo@be.net (usuário comum)
   - mayane@be.net (usuário comum)
   - A senha para todos esses usuários é a mesma: Expert@123

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5078/swagger/ 

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
