[![Build Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)
[![Github Release Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main&label=Github-Release)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)
[![Azure Deployment Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main&label=Azure-Deployment)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)

# Clean Architecture with .NET Core Web API 
Clean Architecture Pattern with .NET Core Web API. BlogWebApi specs can be found [here](http://webapiblog.azurewebsites.net/index.html)

# Getting Started
1. Install the latest version of .NET Core SDK [.NET Core 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/3.1)
1. Install the latest version of Visual Studio [Download](https://visualstudio.microsoft.com/downloads/) 
1. Install the latest version of Docker [Docker Desktop](https://www.docker.com/products/docker-desktop)

# How the code is organized
The solution is organized in the following way

    - BlogWebApi.WebApi.sln
        | - azure-pipelinezz.yml

        | - README.md

        | - src

            | - BlogWebApi.Application

            | - BlogWebApi.Domain

            | - BlogWebApi.Infrastructure

            |- BlogWebApi.WebApi
                | - Dockerfile

        | - tests

            | - Application.UnitTest

            | - Infrastructure.IntegrationTests

            | - WebApi.EndToEndTests
        | -docker-compose
            | - docker-compose.yml

## Domain Project
This project will include Domain Models, Interfaces that will be implemented by the outside layers, enums, etc., specific to the domain logic.  This project should not have any dependecy to another project since it is the core of the project.

### Domain Types
* Domain Models (e.g. `Blog`)
* Interfaces
* Exceptions
* Enums

### Application Project
This project will contain the application logic. The only dependency that should have is the Domain project. Any other project dependency must be removed.

### Appllication Types
* Exceptions (e.g.: `BadRequestException`)
* Interfaces (e.g. `IBlogRepository`)
* DTOs (e.g.: `BlogAddRequestDto`)
* Mappers (e.g: `BlogMapper`)

### Infrastructure Project
The Infrastructure project generally includes data access implementations or accessing external resources as file sytems, SMTP, third-party services, etc.  These classes should implementations of the Interfaces defined in the Domain Project.  Therefore, the only dependency in this project should be to the Domain Project.  Any other dependency must be removed.

### Infrastructure Types
* EF Core Types (e.g.: `BlogDbContext`) 
* Repository Implementation (e.g.: `BlogRepository`)

### WebAPI Project
This is the entry point of our application and it depends on the Application and Infrastrucre projects.  The dependency on Infrastructre Project is requiered to support Dependency Injection in the `Startup.cs` class.  Therefore, no direct instantiation of or static calls to the Infrastucture project should be allowed.

### WebAPI Types
* Controllers (e.g.: `BlogsController`)
* Startup
* Program 

# Build and Test
## Visual Studio
1. Start debugging using Visual Studio (F5)

## .NET CLI
1. From the root directory, execute the following commands
```
    cd src\BlogWebApi.WebApi
```
1. Build the project
```
    dotnet build BlogWebApi.WebApi.csproj
```
1. Run the project
```
    dotnet run BlogWebApi.WebApi.csproj
```

## Test
### Unit Testing
```
    dotnet test tests/Application.UnitTest
```

### Integration Testing
```
    dotnet test tests/Infrastructure.IntegrationTests
```

### End to End Testing
```
    dotnet test tests/WebApi.EndToEndTests
```

## Docker Compose
1.From the root folder, execute the following command:
```
    docker-compose -f docker-compose.yml -f docker-compose.override.yml -p BlogWebApi up --build --remove-orphans --abort-on-container-exit
```

### Certificate Installation

#### Windows
```
    dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p P@ssW0rd!
    dotnet dev-certs https --trust
```

#### MacOS
```
    dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p P@ssW0rd!
    dotnet dev-certs https --trust
```