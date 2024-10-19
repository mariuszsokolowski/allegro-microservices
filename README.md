# Allegro Microservices

## Overview
This project implements a microservices architecture for integrating with the Allegro sandbox API, enabling efficient management of marketplace functionalities.


## Features
- **Authentication**: Secure user authentication and authorization
- **API Gateway**: Centralized routing and management of microservices
- **Product Management**: Full support for CRUD operations for product listings
- **Order Processing**: Handling orders, payments, and status updates

## Architecture
The application follows a microservices architecture, where each service is independently deployable and scalable.

## 🚀 Getting Started
1. Navigate to the folder containing the **docker-compose.yaml** file:
```cmd
cd src
```
2. Open PowerShell (on Windows) or your terminal (on Linux/macOS), and run the following command:
```cmd
docker-compose up --build
```
This will build the images and start all services.

## :rocket: Future Improvements
- [x] **Docker Integration**: Plan to add Docker support for each microservice
- [ ] **Kubernetes Deployment**: Future implementation of Kubernetes for orchestration and management of containerized applications
- [ ] **CI/CD Pipeline**: Implement a Continuous Integration and Continuous Deployment (CI/CD) pipeline to automate testing and deployment processes for the project
