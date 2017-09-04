
Shareable link from Google Drive

To use an image in the software from Google Drive, the image must first be placed inside of the public 'Software-Images'. To properly embed
the image on the web page, make sure that the src is equal to https://drive.google.com/uc?export=view&id= + image ID. The shareable link given
by Google Drive can be used in an iframe, while the software takes img only.



Migrations

When the data model if modified, it gets our of sync with the database. The database needs to be deleted and re-created. 
To do this without losing any data, the Code First Migrations feature solves this problem by updating the database schema 
instead. Go over to 'Package Manager Console' to do this. If not enabled, migrations need to be updated with the command 
'enable-migrations'. To add a new migration, the command is 'add-migrations migration_name', with migration_name being any
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


Publishing

Project is hosted on SmarterASP. Login information for account are:
   Username: mmbabol@gmail.com
   Password: JimmyJim1

To publish the project, go to Build > Publish. Select the SmarterASP profile to use pre-configured options for quick publishing. To get the connection
information, log in to www.smarterasp.net using the above credentials. Inside of the Control Panel, navigate to the Websites tab, and expand the WebDeploy
Info. Make sure that VS STATUS is ON. Choose the Web Deploy method. Take the following fields from the WebDeploy Info section and insert them into Visual Studio:
    Server > Service URL
	Site name > Site/Application SiteName
	User name > Webdeploy Username
	Password > Account password, check password above
	Destination URL > website in the title above, typically Site name + .gtempurl.com

To add a database, navigate to the Database tab. Add a new MSSQL or MySQL Database. Choose an appropriate name and password, as well as the Disk Quota.
The disk quoate can be modified once the database is created. The credentials to the database are as follow:
    Database Name: DB_A25923_Admin3MA
	Username: DB_A25923_Admin3MA_admin
	Password: Hello123#
	
Expand the Connection string examples to see an example of the connection string for publishing. Copy the entire string from the ASP.NET section. To 
attach a database to the project, expand Actions > Attach MDF file to Database. Select upload MDF, then select the database. The database must be an MDF
file. In Visual Studio, open up the Web.config file. Inside of the connectionStrings tag area, modify the connection string to the connection string from 
smarterasp. Make sure to update the correct password in the connection string. When releasing the project, remove the attribute 'debug=true' from the 
compilation tag below. Head over to settings inside of publish web. Paste the database connection string into the space below ApplicationDbContext. Make sure that the password is correct. Select both the checkmarks underneath.

In the preview section, start the preview. This will determine any changes made since the last publish and list the files in a table. Select the desired 
files to publish, then publish the application. A window will then come up with all the new changes.

















