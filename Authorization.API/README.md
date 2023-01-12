	Step by step tutorial how run Authorization.API:
1. Change Server name in appsettings.json AuthorizationDbConnection string;
2. Run commands in PMC:
	update-database -context PersistedGrantDbContext
	update-database -context ConfigurationDbContext
	update-database -context AuthorizationDbContext
3. For seed default users data first run Authorization.API project from PMC by following command:
	dotnet run Authorization.API/Debug/net6.0/Authorization.API /seed --project Authorization.API
