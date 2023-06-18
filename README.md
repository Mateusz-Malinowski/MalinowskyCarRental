# MalinowskyCarRental Manager - The simplest database manager possible

Here is an example of a very simple C# WPF app that allows the user to perform CRUD operations on the database.

## **Prerequisites**

You need the following tools in order to run/edit the solution.

- Microsoft Visual Studio (Latest recommended)

- Microsoft SQL Server (Latest recommended)

## **Getting Started**

The application requires a database to store the data. Follow the below
steps to setup database. 

1. Run the script 'MalinowskyCarRental.sql' in MS SQL SERVER to create and populate database

2. Set the connection string

	1. Open MalinowskyCarRental.sln (Visual Studio is required)
	2. Go to Properties in Solution Explorer
	3. Go to Settings
	4. Insert Name as 'connString', Type as '(Connection String)', Scope as 'Application' and Value as connection string of the database (You can generate the string by connecting to database using 3 dots on the right side of this field).
