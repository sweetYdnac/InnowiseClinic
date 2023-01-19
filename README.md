# InnowiseClinic

Tutorial step by step how run InnowiseClinic application:
1. Run "docker-compose build" command from command line, opened in .sln file directory.
2. Run "docker-compose up" command
3. Open file Authorization.API/appsettings.json
    Change Server name in "AuthorizationDbConnection" from "sqlServer" to "localhost,1442"
4. Run update-database command in PCM (pay attension on default project)
5. Change Server name in "AuthorizationDbConnection" from "localhost,1442" to "sqlServer"