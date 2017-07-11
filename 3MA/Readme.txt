
Migrations

During the duration of the project, when the data model if modified, it gets our of sync with the database. The database needs to be
deleted and re-created. To do this without losing any data, the Code First Migrations feature solves this problem by updating the 
database schema instead. Go over to 'Package Manager Console' to do this. If not enabled, migrations need to be updated with the 
command 'enable-migrations'. To add a new migration, the command is 'add-migrations migration_name', with migration_name being any
desired name for the project to reference to later on. The database then needs to be updated with the command 'update-database'. 


Errors

Process with an Id of #### is not running

-Open Visual Studio as an administrator
-Right click on project and click on unload
-Right click on project and click on open edit ...csproj
-Find the code below and delete it
          <DevelopmentServerPort>0</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>http://localhost:49210/</IISUrl>
-Save and close the file
-Reload the project