<!--
🌐 [English](#-english) | [Português](#-português)
-->

<p align="center">
  <a href="#-português">🇧🇷 Português</a>
  <br/><br/>
  <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt=".NET Badge" />
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C# Badge" />
  <img src="https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white" alt="Angular Badge" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server Badge" />
</p>

<!-- Adicione um banner ou screenshot do seu projeto aqui, se desejar -->
<!-- <img width="1080" alt="Cover" src="URL_DA_SUA_IMAGEM_AQUI"/> -->

---

## 🇺🇸 English

### 🎬 Project Description

**ContactManager** is a full-stack web application developed to provide a simple and efficient way to manage personal or professional contacts. The system allows users to add, edit, delete, and search for contacts through an intuitive and clean interface. The goal is to offer a centralized and organized platform for contact management.

### 🛠️ Tools and Technologies Used

- **Backend:**
  - **.NET 8 / C#** 💻 — Core platform for the API.
  - **ASP.NET Core** 🌐 — Framework for building the REST API.
  - **Entity Framework Core** 🗃️ — ORM for data access to the database.
  - **SQL Server** 🗄️ — Relational database for storing contact information.
  - **AutoMapper** 🔄 — For object-to-object mapping (Entities to DTOs).

- **Frontend:**
  - **Angular** 🅰️ — Framework for building the client-side user interface.
  - **TypeScript** ⌨️ — Main language for the frontend.
  - **HTML & SCSS** 🎨 — For structuring and styling the application.
  - **UI Component Library** ✨ (e.g., Angular Material or PrimeNG).

### 🏛️ Project Architecture

The project is structured following modern software design principles, separating backend and frontend responsibilities.

#### Backend Architecture (Clean Architecture)
The API follows **Clean Architecture** principles, separating concerns into distinct layers:
- **Domain**: Contains the core business logic and entities (e.g., `Contact`), with no external dependencies.
- **Application**: Orchestrates the data flow and implements use cases (services for managing contacts). It depends only on the Domain layer.
- **Infrastructure**: Handles external concerns like database access (EF Core `DbContext`, repository implementations).
- **API**: The entry point of the application, responsible for exposing REST endpoints and handling HTTP requests/responses. It connects all other layers through **Dependency Injection**.

#### Frontend Architecture
The frontend is built with Angular, following its standard architecture:
- **Components**: Reusable UI blocks with their own logic and templates (e.g., contact list, creation/edit form, search bar).
- **Services**: Encapsulate business logic, such as API communication (`ContactService`).

### 🚀 How to Run the Project Locally

#### Backend (.NET API)
1. **Prerequisites:**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
   - [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express edition is sufficient).

2. **Clone the repository:**
   ```bash
   git clone https://github.com/IagoAntunes/ContactManager.git
   ```

3. **Configure the backend:**
   - Navigate to the API's source directory.
   - Update `appsettings.json` with your SQL Server connection string.

4. **Apply database migrations:**
   - Open a terminal in the API's source directory (where the `.csproj` file is).
   - Run the command to create or update the database schema:
     ```bash
     dotnet ef database update
     ```

5. **Run the API:**
   ```bash
   dotnet run
   ```
   The API will be available at a configured port (e.g., `https://localhost:7001`).

#### Frontend (Angular)
1. **Prerequisites:**
   - [Node.js and npm](https://nodejs.org/en/) (LTS version recommended).
   - Angular CLI: `npm install -g @angular/cli`.

2. **Navigate to the frontend folder:**
   ```bash
   cd path/to/your/frontend-folder
   ```

3. **Install dependencies:**
   ```bash
   npm install
   ```

4. **Run the application:**
   ```bash
   ng serve
   ```
   The application will be available at `http://localhost:4200`.

---

## 🇧🇷 Português

### 🎬 Descrição do Projeto

**ContactManager** é uma aplicação web full-stack desenvolvida para fornecer uma maneira simples e eficiente de gerenciar contatos pessoais ou profissionais. O sistema permite aos usuários adicionar, editar, remover e pesquisar contatos através de uma interface intuitiva e limpa. O objetivo é oferecer uma plataforma centralizada e organizada para a gestão de contatos.

### 🛠️ Ferramentas e Tecnologias Utilizadas

- **Backend:**
  - **.NET 8 / C#** 💻 — Plataforma principal da API.
  - **ASP.NET Core** 🌐 — Framework para construção da REST API.
  - **Entity Framework Core** 🗃️ — ORM para acesso a dados do banco de dados.
  - **SQL Server** 🗄️ — Banco de dados relacional para armazenar as informações dos contatos.
  - **AutoMapper** 🔄 — Para mapeamento de objetos (Entidades para DTOs).

- **Frontend:**
  - **Angular** 🅰️ — Framework para construção da interface do cliente.
  - **TypeScript** ⌨️ — Linguagem principal do frontend.
  - **HTML & SCSS** 🎨 — Para estruturação e estilização da aplicação.
  - **Biblioteca de Componentes de UI** ✨ (ex: Angular Material ou PrimeNG).

### 🏛️ Descrição da Arquitetura

O projeto é estruturado seguindo princípios modernos de design de software, separando as responsabilidades de backend e frontend.

#### Arquitetura do Backend (Clean Architecture)
A API segue os princípios da **Clean Architecture**, dividindo as responsabilidades em camadas distintas:
- **Domain**: Contém a lógica de negócio principal e as entidades (ex: `Contact`), sem dependências externas.
- **Application**: Orquestra o fluxo de dados e implementa os casos de uso (serviços para gerenciar contatos). Depende apenas da camada de Domínio.
- **Infrastructure**: Lida com preocupações externas, como acesso ao banco de dados (implementação do `DbContext` do EF Core, repositórios).
- **API**: Ponto de entrada da aplicação, responsável por expor os endpoints REST e lidar com requisições/respostas HTTP. Conecta todas as outras camadas através de **Injeção de Dependência**.

#### Arquitetura do Frontend
O frontend é construído com Angular, seguindo sua arquitetura padrão:
- **Components**: Blocos de UI reutilizáveis com sua própria lógica e templates (ex: lista de contatos, formulário de criação/edição, barra de busca).
- **Services**: Encapsulam a lógica de negócio, como a comunicação com a API (`ContactService`).

### 🚀 Como Rodar o Projeto Localmente

#### Backend (.NET API)
1. **Pré-requisitos**:
   - [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
   - [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (a edição Express é suficiente).

2. **Clone o repositório:**
   ```bash
   git clone https://github.com/IagoAntunes/ContactManager.git
   ```

3. **Configure o backend**:
   - Navegue até o diretório fonte da API.
   - Atualize o arquivo `appsettings.json` com sua connection string do SQL Server.

4. **Aplique as migrações do banco de dados**:
   - Abra um terminal no diretório fonte da API (onde o arquivo `.csproj` se encontra).
   - Execute o comando para criar ou atualizar o esquema do banco de dados:
     ```bash
     dotnet ef database update
     ```

5. **Execute a API**:
   ```bash
   dotnet run
   ```
   A API estará disponível em uma porta configurada (ex: `https://localhost:7001`).

#### Frontend (Angular)
1. **Pré-requisitos**:
   - [Node.js e npm](https://nodejs.org/en/) (versão LTS recomendada).
   - Angular CLI: `npm install -g @angular/cli`.

2. **Navegue até a pasta do frontend**:
   ```bash
   cd caminho/para/sua/pasta-frontend
   ```

3. **Instale as dependências**:
   ```bash
   npm install
   ```

4. **Execute a aplicação**:
   ```bash
   ng serve
   ```
   A aplicação estará disponível em `http://localhost:4200`.

---
<p align="center">
  <a href="#-english">⬆️ Back to top</a>
</p>
