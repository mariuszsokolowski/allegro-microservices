# GitHub Actions Workflows

This page describes the CI/CD workflows for allegro-microservices using [GitHub Actions](https://github.com/mariuszsokolowski/allegro-microservices/actions).

## Workflow
### 1. 🔨 `dontet-build`

This workflow is responsible for manually building the .NET microservices (`authservice`, `productservice`, and `orderservice`) on each push or pull request to the `master` branch. 

#### Workflow Details:
- **Trigger**: 
  - Runs on every push and pull request to the `master` branch.
- **Runs on**: `ubuntu-latest`
- **Steps**:
  1. Checks out the repository code using the `actions/checkout@v2`.
  2. Sets up the .NET SDK version `8.x` using the `actions/setup-dotnet@v1`.
  3. Builds each microservice (`authservice`, `productservice`, `orderservice`) individually by navigating to their respective directories and running `dotnet build` in Release configuration.

#### Workflow File: `dotnet-build.yml`

### 2. 🐋 `docker-build`

This workflow handles the building of Docker images for the three microservices (authservice, productservice, and orderservice). It builds Docker images for these services on every push or pull request to the master branch.
#### Workflow Details:
- **Trigger**:
  - Runs on every push and pull request to the master branch.
- **Runs on**: ubuntu-latest
- **Steps**:
  1. Checks out the repository code using actions/checkout@v4.
  2. Builds the Docker image for each microservice by navigating to their respective directories and running docker build.

#### Workflow File: `docker-build.yml`
