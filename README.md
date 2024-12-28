# Store Warehouse

## Project Description

The Store Warehouse project is a comprehensive solution for managing items and warehouses. It includes multiple projects, each with a specific role in the overall system:

- `warehouse.service.api`: Contains controllers for managing items and warehouses.
- `warehouse.service.domain`: Defines the domain models and repository interfaces.
- `warehouse.service.business`: Contains use cases for items and warehouses.
- `warehouse.service.entityframework`: Includes the Entity Framework context and repository implementations.

The project uses MediatR for handling commands and queries, and includes configuration files for Docker and GitHub Actions.

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- Docker
- SQL Server

### Running the Project

1. Clone the repository:
   ```sh
   git clone https://github.com/phferreira13/store-warehouse.git
   cd store-warehouse
   ```

2. Build and run the Docker containers:
   ```sh
   docker-compose up --build
   ```

3. The API will be available at `http://localhost:5000`.

## Usage Examples

### API Endpoints

#### Items

- **Get all items**
  ```http
  GET /api/items
  ```

- **Get item by ID**
  ```http
  GET /api/items/{id}
  ```

- **Create item**
  ```http
  POST /api/items
  Content-Type: application/json

  {
    "name": "Item Name",
    "price": 10.0,
    "description": "Item Description"
  }
  ```

- **Update item**
  ```http
  PUT /api/items/{id}
  Content-Type: application/json

  {
    "name": "Updated Item Name",
    "price": 15.0,
    "description": "Updated Item Description"
  }
  ```

#### Warehouses

- **Get all warehouses**
  ```http
  GET /api/warehouses
  ```

- **Get warehouse by ID**
  ```http
  GET /api/warehouses/{id}
  ```

- **Create warehouse**
  ```http
  POST /api/warehouses
  Content-Type: application/json

  {
    "name": "Warehouse Name",
    "location": "Warehouse Location",
    "items": [
      {
        "itemId": 1,
        "quantity": 100
      }
    ]
  }
  ```

- **Update warehouse**
  ```http
  PUT /api/warehouses/{id}
  Content-Type: application/json

  {
    "name": "Updated Warehouse Name",
    "location": "Updated Warehouse Location"
  }
  ```

## Repository Structure and Key Components

- `warehouse.service.api`: Contains the API controllers and configuration files.
- `warehouse.service.domain`: Defines the domain models and repository interfaces.
- `warehouse.service.business`: Contains the business logic and use cases.
- `warehouse.service.entityframework`: Includes the Entity Framework context and repository implementations.

## Running the Project with Docker and GitHub Actions

### Docker

The project includes a `Dockerfile` and a `docker-compose.yml` file for building and running the application in Docker containers. To build and run the containers, use the following command:

```sh
docker-compose up --build
```

### GitHub Actions

The repository includes configuration files for GitHub Actions to automate the build and deployment process. The workflows are defined in the `.github/workflows` directory.

- `docker-image.yml`: Builds and pushes the Docker image to DockerHub.
- `store-warehouse.yml`: Compiles and deploys the .NET Core application to an Azure Web App and imports the API into Azure API Management.

