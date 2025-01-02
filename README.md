# WebAPI com .NET 8 e Swagger

## Descrição do Projeto

Este projeto é uma WebAPI desenvolvida com **.NET 8**, utilizando a metodologia **DDD** (Domain-Driven Design) e **SQL Server** como banco de dados. A aplicação utiliza **JWT (JSON Web Token)** para autenticação, garantindo a segurança no acesso às rotas.

### Funcionalidades Principais:
- **Autenticação (Auth)**: Obtenção de token de acesso.
- **Gerenciamento de Usuários (User)**: Criação de novos usuários.
- **Gerenciamento de Notícias (News)**: Operações de CRUD protegidas por autenticação.

---

## Requisitos

- **.NET 8**
- **SQL Server**
- **Executar as migrations disponíveis no projeto WebAPI**
- **Token JWT para acesso às rotas de notícias (News). O token é gerado após a criação de um usuário e autenticação na rota Auth usando o Bearer Token.**

---

## Configuração do Projeto

### 1. Configuração do Banco de Dados
Antes de iniciar o projeto, certifique-se de aplicar as migrations disponíveis para criar as tabelas no banco de dados.

```bash
Add-Migration Inicial -Context AppDbContext

Update-Database -Context AppDbContext
```

---

## Endpoints Disponíveis

![image](https://github.com/user-attachments/assets/a9669722-2058-4b1d-ae95-4505649f1851)

### 1. Auth
**Endpoint:** `POST /api/Auth/CreateToken`  
**Descrição:** Gera um token JWT para acesso às rotas protegidas.

### 2. User
**Endpoint:** `POST /api/User`  
**Descrição:** Cria um novo usuário.

### 3. News
Todas as operações relacionadas às notícias exigem autenticação via JWT.

#### 3.1 Listar Notícias
**Endpoint:** `GET /api/News`  
**Descrição:** Retorna todas as notícias cadastradas.

#### 3.2 Criar Notícia
**Endpoint:** `POST /api/News`  
**Descrição:** Cria uma nova notícia.

#### 3.3 Atualizar Notícia
**Endpoint:** `PUT /api/News`  
**Descrição:** Atualiza uma notícia existente.

#### 3.4 Deletar Notícia
**Endpoint:** `DELETE /api/News`  
**Descrição:** Exclui uma notícia existente.
