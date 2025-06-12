🚀 A Simple Yet Modern Microservice Demo Project

This project demonstrates many modern software architecture and integration techniques in a simple structure. Here are the details:

🔧 Infrastructure & Container Setup

The infrastructure is configured using Docker Compose.

Database Services:

- Order Service → MSSQL (Docker image)

- Stock Service → MongoDB (Docker image)

- Stock Service → MongoDB UI (Docker image)

- RabbitMQ (Docker image)

📦 Services & Architectures

Order Service

- Built with Minimal API

- Implements Vertical Slice Architecture

- Simplified service definitions using extension methods (endpoints are defined using RouteGroupBuilder)

Stock Service

- MongoDB + EF Core integration

- Configuration management using the Options Pattern

Payment Service

- Simple structure focused solely on message consumption

📨 Event-Driven Communication

- Uses MassTransit + RabbitMQ

- Both Send and Publish methods are implemented

- Acknowledgement management and message durability (written to physical disk) are ensured

- Inter-service consistency is maintained through Choreography-Based Saga and the Eventual Consistency principle

