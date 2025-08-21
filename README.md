# HahnSoftwareDemo

## About

This is a demo application showcasing a simple backend API using .NET and a frontend built with Vue.js. The application demonstrates Clean Architecture, CQRS principles, and integration between backend and frontend.

## Technologies

* **Backend:** .NET 9, EF Core
* **Frontend:** Vue.js, TypeScript, JavaScript
* **Containerization:** Docker, Docker Compose

## Run with Docker
```bash
docker-compose up --build
````
#### API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
#### Frontend: [http://localhost:8080](http://localhost:8080)

## Local Development

### Backend

```bash
cd src/Api
dotnet run
```

### Frontend

```bash
cd frontend
npm run dev
```

