# 🍔 API de Lanchonete

Uma API desenvolvida para gerenciar pedidos e operações de uma lanchonete, utilizando tecnologias modernas e escaláveis.

## 🚀 Tecnologias Utilizadas

- **Visual Studio** - Ambiente de desenvolvimento
- **.NET 9** - [SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/thank-you/sdk-9.0.203-windows-x86-installer)
- **Swagger** - Documentação interativa (Swashbuckle.AspNetCore)
- **AutoMapper** - Mapeamento de objetos (v12.0)
- **AutoMapper.Extensions.Microsoft.Dep** - Extensões para AutoMapper (v12.0)
- **Kafka** - Mensageria escalável (Confluent.Kafka)
- **Docker** - Containerização
  - **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop/)
  - **docker-compose** - Orquestração de containers
  - **Imagem Zookeeper** - [Docker Hub](https://hub.docker.com/_/zookeeper)

## 🏗 Arquitetura

- **Clean Architecture** - Estrutura modular e escalável
- **Mensageria com Kafka** - Comunicação assíncrona para alta latência

## 🛠 Instalação

### 1️⃣ Clone o repositório
```sh
https://github.com/Eduardooliveira95/apl-lanchonete-pedido.git
cd api-lanchonete
```

### 2️⃣ Instale as dependências
```sh
../apl-lanchonete-pedido/LanchoneteApi dotnet restore
```

### 3️⃣ Inicialize o Docker
```sh
docker-compose up -d
```
### 4️⃣ Execute a aplicação
```sh
dotnet run
```
### 5️⃣ Acesse a documentação Swagger
```sh
http://localhost:5086/swagger
```
### Agradeço!
