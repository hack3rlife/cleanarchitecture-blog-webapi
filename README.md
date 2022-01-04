[![Build Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)
[![Github Release Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main&label=Github-Release)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)
[![Azure Deployment Status](https://hack3rlife.visualstudio.com/Github/_apis/build/status/hack3rlife.cleanarchitecture-blog-azure?branchName=main&label=Azure-Deployment)](https://hack3rlife.visualstudio.com/Github/_build/latest?definitionId=5&branchName=main)

# Clean Architecture with .NET Core Web API 
Clean Architecture Pattern with .NET Core Web API.  Swagger docs can be found [here](http://webapiblog.azurewebsites.net/index.html)

# How the code is organized
The solution is organized in the following way

    - BlogWebApi.WebApi.sln

        | - README.md

        | - src

            | - BlogWebApi.Application

            | - BlogWebApi.Domain

            | - BlogWebApi.Infrastructure

            |- BlogWebApi.WebApi

        | - tests

            | - Application.UnitTest

            | - Infrastructure.IntegrationTests

            | - WebApi.EndToEndTests


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