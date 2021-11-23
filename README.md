ToDoListAPI
-----------


PreRequisites
-------------

Visual Studio Code,

.Net Core SDK 3.1.414 or later,

SQL Server Express 2017 or later

Steps
-----

Code Setup
----------

1. Download ToDoAPI repository from
https://github.com/nit2021/ToDoListAPI.git
2. After extraction open folder select folder "ToDoListAPI-master"  and open with VS code.
3. Open Terminal
4. Run command below
	
	"dotnet restore"

DB Setup
--------
1. Run below command interminal to set path

	"cd .\ToDoAPI"
	
2. Run below below in terminal to add ef
	
	"dotnet tool install --global dotnet-ef"

3. Run command below in terminal to create migration
	
	"dotnet ef --startup-project ./ToDo.API.csproj migrations add NewMigrationToDo --context ToDoContext --output-dir Migrations --project ../ToDo.DAL/ToDo.DAL.csproj"
	
4. Run command below in terminal to create database in SQL Express
	
	"dotnet ef database update"

The above command will create database with one default username:"Admin" and PassWord="Pa$$w0rd" in it.


Optional - "To Add more users/tables data"
------------------------------------------

A) You can add more user/tables data by visiting ModelBuilderExtensions.cs file then in Seed method you can add seeding entity data to respective class. For example, for seeding user record you can add below line

modelBuilder.Entity<User>().HasData(new User { UserId = {102}, UserName = "{Username}", Password = "{userpassword});
 

B) Add new migration name to below command and run command in terminal
	
	"dotnet ef --startup-project ./ToDoAPI.csproj migrations add New_Migration_Name --context ToDoContext --output-dir Migrations --project ../ToDoDAL/ToDoDAL.csproj"
	

C) Run command below in terminal to update database in SQL Express
	
	"dotnet ef database update"


Hosting Application  
-------------------
	
1. Run command below to host application
	
	"dotnet run"
	
Using Swagger
-------------
	
1. Go to https://localhost:5001/swagger/index.html 
2. Click Authorize button, fill username and password in popup window. 
3. Click login then close credential pop up windows
4. Execute respective endpoint after clicking Try it out then execute.

You can set different application url in launchsetting.json in case you are already using some port or want to provide different port.
	
Using GraphQL
-------------
1. Go to https://localhost:5001/graphql/
2. Add below Authorization to Http Headers 
	For user "Admin"

	{
    		"Authorization": "Basic QWRtaW46UGEkJHcwcmQ="
	}
	
	For user "Standard"

	{
    		"Authorization": "Basic U3RhbmRhcmQ6UGEkJHcwcmQy"
	}
	
3. Write quey or mutation command using schema in schema window for respective function. For example for item creation -
	
	mutation
	{
	  createListItem(
	    itemDesc:"itemgraphqlnew"
	    )
	    {
		    description,
		    createdDate
	    }
	}
4. Click on execute button and run the command. 
