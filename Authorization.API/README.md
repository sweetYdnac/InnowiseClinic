	Step by step tutorial how run Authorization.API:
1. Change Server name in appsettings.json AuthorizationDbConnection string;
2. Uncomment in AuthorizationDbContext this one
	//builder.ApplyConfiguration(new IdentityRoleConfiguration())
3. Run update-database command in PMC:
