# ASP.NET WebForms and MVC Application README

## Application Overview:

This project involves creating an ASP.NET application using WebForms and MVC frameworks, targeting .NET Framework 4.8.x. It interacts with a SQL Server database comprising three tables: Companies, Contacts, and Addresses.

### Data Operations:

- CRUD operations on Companies, Contacts, and Addresses are facilitated via stored procedures.
- Add/Edit operations use full postbacks, while delete operations utilize AJAX requests.

### Data Validation and AJAX Event:

- Basic data validation is implemented.
- At least one AJAX-based event, such as asynchronous deletion, is included.

## MVC Application (ASP.NET Framework 4.8.x)

### Conversion to MVC:

- The WebForms application is converted to an MVC application.

### Entity Framework Data Model:

- The data model is generated from the existing SQL Server database using Entity Framework in "database first" mode.

### Functional Requirements:

- The MVC version maintains the same CRUD functionality as the WebForms application.

## Usage:

1. Set up a SQL Server database with the specified schema.
2. Configure the connection string in `web.config`.
3. Build and run the application.
