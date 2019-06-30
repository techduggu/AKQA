# AKQA .NET Tech Challenge

This repository contains .NET application solution for AKQA .NET Tech Challenge built using ASP.NET Core, WebApi and Entity Framework Core. The architecture and design of the project are based on very popular 'Clean Architecture' which is very well explained in the following links:

* [The Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
* [Common web application architectures by Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/modern-web-apps-azure-architecture/common-web-application-architectures)
* [Architect Modern Web Applications with ASP.NET Core by Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/modern-web-apps-azure-architecture/)

The different projects created as part of Clean Architecture under this repository contains a ReadMe.md file for further information.

## Getting Started
Use these instructions to get the project up and running:

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  
  2. Open the `AKQA.sln` solution in Visual Studio 2017
  
  3. Restore the nuget packages and build the solution
  
  4. Next, under `Web`directory, right-click on `AKQA.Api` project and select 'Set as StartUp Project'. Hit play button (with IIS Express) on Visual Studio to launch the AKQA Web Api. It will display the Swagger UI of AKQA Web Api (which can also be used to test the API endpoints).
	 Note: This step is required only for the first time to launch/host the WebApi in IIS Express.
  
  5. Once the AKQA.Api has started, within the `Web` directory, Now set `AKQA.Web` as StartUp Project and launch it using Visual Studio. It will display the AKQA Home page.

  5. Later, you can also launch [https://localhost:44324/](https://localhost:44324/) in your browser to view the Web UI
  
  6. Launch [https://localhost:44343/swagger](https://localhost:44343/swagger) in your browser to view the API

  ### Additional Information:

  1. The `AKQA.Web` project calls `AKQA.Api` using `site.js`. If IIS Express allocates different port number to localhost in your machine, please configure the API URL under `site.js`. This is just for dev setup and should be moved to appsettings configuration in PROD environment with WebAPI hosted.

  2. One Assumption made: Customer data should also persist or stored in the system. On application start, it uses SQL local DB ((localdb)\mssqllocaldb) provided by Visual Studio. 

  3. The `Tests` directory contains Application unit and Integration Test Cases. You can use Visual Studio (Test -> Run -> All Tests) feature to execute the tests.

## Technologies
* .NET Core 2.1
* ASP.NET Core 2.1
* Entity Framework Core 2.1
* xunit
* [Moq](https://www.nuget.org/packages/Moq/)
* [MediatR with Command Design Pattern](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/aspnet)
* [Swashbuckle.Core - Swagger for WebApi](https://www.nuget.org/packages/Swashbuckle.Core/)
* [Humanizer](https://www.nuget.org/packages/Humanizer)

## Known Issues/Future Improvements
* Encryption for Customer PII Data
* Caching
* Security (e.g. Anti-Forgery, Authentication, CORS etc.)
* Client-side Validation using UI Framework (e.g. jQuery, Angular etc.)