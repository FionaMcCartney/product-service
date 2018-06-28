# Introduction
An ASP.NET application that uses the Entity Framework with code first migrations and a seed method that loads the products.

# Getting Started
To create the Entity Framework database in your LocalDB, in the Package Manager Console window:
1.	Add any missing NuGet Packages as reported by Visual Studio
2.  Execute the following to generate the code that creates the database: Add-Migration Initial
3.	Execute the following to run the code: Update-Database

To ensure the products.json file is found, update the file path in \Migrations\Configuraion.cs

# Build and Test
Run the application through the debugger (F5)