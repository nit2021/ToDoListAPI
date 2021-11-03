ToDoListAPI

PreRequisites
Visual Studio Code
.Net Core SDK 3.1.414 or later
SQL Server Express

Steps

Code SetUp
----------

1. Download ToDoAPI repository from
https://github.com/nit2021/ToDoListAPI
2. After extraction open folder select folder "ToDoListAPI-master"  and open with VS code.
3. Open Terminal
4. Run command dotnet restore
5. Run below command to setup DB

DB SetUp
6. Run command below in terminal to create migration
	dotnet ef migrations add Initial --context ToDoContext
7. Run command below in terminal to create database in SQL Express
	dotnet ef database update

The above command will create database with one default username:"Admin" and PassWord="PaSSw0rd" in it.

Optional[To Add more users]
--------
A) You can add more user by visiting ToDoContext.cs file then in OnModelCreating method add below line and save.
modelBuilder.Entity<User>().HasData(new User { UserId = {101}, UserName = "{Username}", Password = "{userpassword});
Repeat 
B) Add new migration name to below command and run command in terminal
	dotnet ef migrations add {New Migration Name} --context ToDoContext
C) Run command below in terminal to update database in SQL Express
	dotnet ef database update

Running Application
-------------------
8. Run command below to host application
	dotnet run
9. Go to https://localhost:5001/swagger/index.html 
10. Click Authorize to with username and password. 
11. Close credential pop up windows
12. Execute respective endpoint after clicking Try it out.

You can set different application url in launchsetting.json in case you are already using some port or want to provide different port.


