# NetCoreWebAPICourse
This repository will provide step by step course on how to create Restful web API with .net core 3.1 and entity framework

- Web API
- Business services
- EF Core code first database
- EF Core data access
- Repository pattern
- Jwt authorization
- Error handling
- Unit testing

# Pokemon API
<p>The app should be implemented using .NET Core Web API and MSSQL database.</p>

### Prerequisites
<p>Clone repo</p>
<p>Make sure you have admin rights</p>
<p>Install latest SQL Server (MSSQL)</p>

# Backend project:
## Technologies: 
* .Net Core 3.1
* SignalR
* MSSQL (latest)
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
