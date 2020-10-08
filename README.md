# NetCoreWebAPICourse
This repository is sandbox for training and providing course on how to create Restful web API with .net core 3.1 and entity framework or document databases such as mongo

This course is ment to provide following knowledge:

### Introduction
- Basic programming principles and paradigms
- Web API - REST
- Business services and logic
- EF Core code first database
- EF Core data access
- Repository pattern
- Error handling
- Logging 
- Unit testing 

### Intermediate concepts
- Authorization / Authentication
- Integration / Functional tests
- Web sockets
- Event sourcing 
- SQL Profiling & optimization
- Worker services
- Document databases
- Reflection & Expression trees
- Structured Logging
- Containerization

### Go to production
- Deploying 
- CI/CD pipeline management

### More advanced stuff
- GraphQL
- Microservice architecture
- API Gateway



# Pokemon API
<p>The app should be implemented using .NET Core Web API and MSSQL database.</p>

### Prerequisites
<p>Clone repo</p>
<p>Make sure you have admin rights</p>
<p>Install latest Postgre SQL (PgSQL)</p>

# Backend project:
## Technologies: 
* .Net Core 3.1
* SignalR
* PgSQL (latest)
* EF Core
* Automapper
* Swagger 

## Build and run:
I was using VS19 so if you're using something else say VSCode you'll have to use CLI probably to first restore nuget packages and then run startup project
* Open project
* Set Backend.API project as startup
* Create appsettings.Development.json -> Use appSettings.json as cook-book
* Start http://localhost:44320/api/index.html
* Database should automatically seed.
  * If you get stuck further trouble shooting might include checking your Connection string (appsettings and DbContext implementation), privilages and perhaps to manually migrate db with dotnet ef core tools (CLI) 

### To run test
* Open test project and run unit tests or write `dotnet test`
