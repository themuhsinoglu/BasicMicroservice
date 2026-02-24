# 🧩 BasicMicroservice

> A .NET-based microservice demo project showcasing modern software architecture patterns in a clean and approachable structure.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-100%25-239120?style=flat-square&logo=csharp)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-MassTransit-FF6600?style=flat-square&logo=rabbitmq)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

---

## 📖 About

This project is an educational microservice sample that demonstrates modern .NET ecosystem concepts such as **Minimal API**, **Vertical Slice Architecture**, **Event-Driven Communication**, and **Choreography-Based Saga** — all under one roof.

By exploring this project you can learn:

- Asynchronous inter-service communication
- RabbitMQ integration with MassTransit
- Polyglot persistence (different databases per service)
- Clean architecture using Options Pattern and Extension Methods
- Infrastructure management with Docker Compose

---

## 🏗️ Architecture Overview

```
┌──────────────────────────────────────────────────────────┐
│                      Client / API                        │
└─────────────────────────┬────────────────────────────────┘
                          │
          ┌───────────────┼───────────────┐
          ▼               ▼               ▼
  ┌──────────────┐ ┌─────────────┐ ┌───────────────┐
  │ Order Service│ │Stock Service│ │Payment Service│
  │  (Minimal API│ │  (MongoDB + │ │  (Consumer    │
  │   + MSSQL)   │ │   EF Core)  │ │   Only)       │
  └──────┬───────┘ └──────┬──────┘ └──────┬────────┘
         │                │               │
         └────────────────┴───────────────┘
                          │
              ┌───────────▼───────────-┐
              │  RabbitMQ (MassTransit │
              │   Send + Publish)      │
              └────────────────────────┘
```

Services **never communicate via direct HTTP calls**. All inter-service communication happens through RabbitMQ events, ensuring **Loose Coupling** and **Eventual Consistency**.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Language / Framework | C# / .NET 8 |
| API Style | Minimal API |
| Architecture Pattern | Vertical Slice Architecture |
| Messaging | MassTransit + RabbitMQ |
| Saga Pattern | Choreography-Based Saga |
| Order Database | MSSQL (Docker) |
| Stock Database | MongoDB + EF Core (Docker) |
| MongoDB UI | Mongo Express (Docker) |
| Containerization | Docker Compose |

---

## 📦 Services

### 🛒 Order Service
- Lightweight HTTP layer built with **Minimal API**
- Features are isolated using **Vertical Slice Architecture**
- Clean and readable endpoint definitions via `RouteGroupBuilder` and extension methods
- Database: **MSSQL**

### 📦 Stock Service
- **MongoDB** + **EF Core** integration
- Application settings managed via the **Options Pattern**
- Responsible for updating stock levels in response to incoming events

### 💳 Payment Service
- Simple, focused structure built purely for message consumption
- Listens for and processes payment confirmation events

### 🔗 Shared Library
- `BasicMicroservice.Shared`: shared event contracts and types used across services

---

## 📨 Event-Driven Communication

The project leverages **MassTransit** with both `Send` and `Publish` messaging approaches:

- **Send** → Delivers a message to a specific queue and a specific receiver
- **Publish** → Broadcasts an event; all subscribed consumers receive it

The **Choreography-Based Saga** pattern coordinates the business workflow across services without a central orchestrator. This approach:

- Keeps each service fully independent
- Increases durability — messages are written to physical disk
- Guarantees consistency via the **Eventual Consistency** principle

---

## 🐳 Getting Started

### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop) & Docker Compose
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### 1. Clone the Repository

```bash
git clone https://github.com/themuhsinoglu/BasicMicroservice.git
cd BasicMicroservice
```

### 2. Start the Infrastructure (Docker Compose)

```bash
docker compose up -d
```

This will spin up the following containers:

| Container | Port |
|---|---|
| MSSQL | 1433 |
| MongoDB | 27017 |
| Mongo Express (UI) | 8081 |
| RabbitMQ | 5672 (AMQP), 15672 (Management UI) |

### 3. Run the Services

Start each service in a separate terminal:

```bash
# Order Service
cd src/services/OrderService
dotnet run

# Stock Service
cd src/services/StockService
dotnet run

# Payment Service
cd src/services/PaymentService
dotnet run
```

### 4. Management Interfaces

| Interface | URL |
|---|---|
| RabbitMQ Management | http://localhost:15672 (guest / guest) |
| Mongo Express | http://localhost:8081 |

---

## 📁 Project Structure

```
BasicMicroservice/
├── shared/
│   └── BasicMicroservice.Shared/       # Shared event contracts
├── src/
│   └── services/
│       ├── OrderService/               # Order service (Minimal API + MSSQL)
│       ├── StockService/               # Stock service (MongoDB + EF Core)
│       └── PaymentService/             # Payment service (Consumer only)
├── docker-compose.yaml                 # Infrastructure definitions
├── .gitignore
├── LICENSE
└── README.md
```

---

## 🧠 Learning Outcomes

By studying this project, you will gain hands-on experience with:

- Clean and fast endpoint definitions using Minimal API
- Feature-based folder organization with Vertical Slice Architecture
- RabbitMQ integration via MassTransit (Send & Publish)
- Inter-service coordination with Choreography-Based Saga
- Eventual Consistency principle in practice
- Combined usage of MongoDB and EF Core
- Configuration management with Options Pattern
- Multi-service orchestration with Docker Compose

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

<p align="center">
  <i>Made by <a href="https://github.com/themuhsinoglu">themuhsinoglu</a></i>
</p>
