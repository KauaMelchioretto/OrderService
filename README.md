# OrderService

Este projeto é um serviço de pedidos construído com .NET 8 e .NET Core, utilizando Docker, PostgreSQL, RabbitMQ e MassTransit para processamento assíncrono.

## Como Subir o Projeto

### Pré-requisitos
- Docker
- Docker Compose

### Passos
1. Clone o repositório.
2. Navegue até a pasta raiz do projeto (onde está o `docker-compose.yml`).
3. Execute o comando:
   ```
   docker-compose up --build
   ```
4. Aguarde os serviços iniciarem (Postgres, RabbitMQ, API e Worker). Pode levar alguns minutos devido aos healthchecks.

OBS.: Quando estiver sendo exibido logs no formato 
'info: OrderService.Worker.Worker[0]
orders_worker    |       Worker running at: MM/dd/yyyy hh:mm:aa +00:00' significa que o projeto inteiro esta Ok para testes.

A API estará disponível em `http://localhost:5000`.

## Como Testar o Projeto

### Via Swagger
1. Após subir o projeto, acesse `http://localhost:5000/swagger/index.html`.
2. Use a interface para testar os endpoints:
   - `GET /orders`: Lista todos os pedidos.
   - `POST /orders`: Cria um novo pedido (forneça CustomerName, Description e Value).

### Via Postman
1. Configure o Postman para enviar requisições para `http://localhost:5000/orders`.

#### GET /orders
- Método: GET
- URL: `http://localhost:5000/orders`
- Descrição: Retorna a lista de pedidos.

#### POST /orders
- Método: POST
- URL: `http://localhost:5000/orders`
- Headers: `Content-Type: application/json`
- Body (JSON):
  ```json
  {
    "customerName": "João Silva",
    "description": "Pedido de teste",
    "value": 100.50
  }
  ```
- Descrição: Cria um novo pedido. O Worker processará assincronamente e atualizará o status para "Processed" em média de 5 segundos após a criação do pedido.

## Estrutura do Projeto

O projeto segue uma arquitetura em camadas, dividido em múltiplos projetos .NET.

### Projetos (.csproj)
- **OrderService.Api**: API Web com ASP.NET Core. Contém controllers, configurações de Swagger e injeção de dependências.
- **OrderService.Worker**: Serviço de background com MassTransit. Consome mensagens do RabbitMQ e processa pedidos.
- **OrderService.Shared**: Biblioteca compartilhada. Contém entidades, enums, eventos e DTOs comuns.
- **OrderService.Infrastructure**: Camada de infraestrutura. Contém o DbContext do Entity Framework e repositórios.

### Diretórios
- **src/**: Código fonte.
  - **OrderService.Api/**: Controllers, Program.cs, appsettings.json, Dockerfile.
  - **OrderService.Worker/**: Worker.cs, Consumers, Program.cs, appsettings.json, Dockerfile.
  - **OrderService.Shared/**: Entities, Enums, Events.
  - **OrderService.Infrastructure/**: Data (DbContext), Repositories.
- **docker-compose.yml**: Configuração dos serviços Docker (Postgres, RabbitMQ, API, Worker).
- **OrderService.sln**: Solução .NET.