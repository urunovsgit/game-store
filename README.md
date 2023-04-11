# GameStore project
This solution is based on ASP.NET Core MVC framework to demonstrate its basic features and show implementation processes on it.

## Purpose of project
The goal of the project is Develop online marketplace for buying and selling video games
and provides functionality that enables users to:
- be authenticated and authorized;
- view all games;
- add, edit and delete game;
- add, edit and delete related comments;
- cart managing and making orders;
- search and set filter options.

Application's demo version is available on Microsoft Azure platform through link:
https://game-store.azurewebsites.net/

## Deployment requirements
Application's deployment configuration:
- Target framework - .NET 7.0;
- SQL Server.

Application deployment instructions available through [this link.](https://learn.microsoft.com/en-us/azure/app-service/tutorial-dotnetcore-sqldb-app)

## Used packages and libraries
	- Microsoft ASP.NET Core Identity Entity Framework Core 7.0.3.
	- Microsoft Entity Framework Core SqlServer 7.0.3.
	- Microsoft Entity Framework Core Tools 7.0.3.
	- Microsoft Entity Framework Core Proxies 7.0.3.
	- Microsoft Entity Framework Core Design 7.0.4.
  - Microsoft ASP.NET Core Identity 2.2.0.
	- AutoMapper Extension MSDI 12.0.0.
	- Newtonsoft.Json 13.0.2


## Application architecture
The solution is based on Model-View-Controller ([MVC](https://en.wikipedia.org/wiki/Model-view-controller)) software architectural pattern which 
allows to separate internal representations of information from the ways information is presented to and accepted from the user. It's achieved through the
seperation of logic into the three components (Model, View and Controller). This pattern has been selected for the following benefits:
- faster development process
- easily modifiable
- ability to provide multiple views
- supports TTD (test-driven development) and others.

Also The MVC framework is easy to implement as it offers above given numerous advantages. 
Projects that are developed with the help of the MVC model can be easily developed with lesser expenditure and within less time too. 
Above all, its power to manage multiple views makes MVC the best architecture pattern for developing web applications.


### Structure (logic separation)
The solution containing three VS projects:
1. A class library project which contains classes that represent the data access layer ([game-store-domain](./game-store-domain/)).
2. A business logic layer ([todo-domain-entities.Test](./game-store-business/)).
3. A client ASP.NET Core MVC application is the UI layer ([game-store](./game-store/)).


### Data access layer
To store the data about ToDo lists is used a Localdb under the management of MSSQL server. The access to the database and managing data providing 
by [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) object-relational mapping tool which allows to create a database without designing the tables directly. This aproach is called 
Code-First helps to create a new database and gives a possibility of changing the configuration of a database upon changing the classes. 
It enables the developers to get more control through the code only.

There are 8 entity types which represent data access objects (DAO) of a current layer ([DAO Entities](./game-store-domain/Entities/)).
For each DAO type is created Repository classes implementing `IRepository` interface that declares CRUD commands, thus any data manipulation will be performed 
by corresponding repository class object. Also `GSUnitOfWork` class is the implementation of Unit of Work pattern which used to group one or more operations 
into a single transaction.

Class `GameStoreDbContext` derives `IdentityDbContext<GameStoreUser, IdentityRole<int>, int>` which is the primary class that is responsible for interacting with the database. 
`AppDbContext` class contains `DbSet<ToDoList>` and `DbSet<ToDoEntry>` properties which represents the collection of entities in the context, 
or that can be queried from the database. Also it accepts context configuration options passed through constructor.


### Business logic layer

This layer consist of three main parts:
  - models - data transfer  representation of DAL objects ([DTO models](./game-store-business/Models/));
  - service classes which provide business functionality ([Services](./game-store-business/ServiceProviders/));
  - mapping profile - mapping DTO objects into DAL and vice versa ([Mapping](./game-store-business/Infrastructure/)).

### Presentation logic layer
The UI part of application is located in ASP.NET Core MVC framework based [Game-store](./game-store/) project. This layer contains following:
  - models - adopted version of DTO objects for UI actions as ViewModels;
  - controllers - classes that reacts user actions and performs business functions;
  - views - razor web-pages which provides UI.

#### Configuration
In `Programm` class configuring services to work with Controllers/Views and loading database access settings from [appsettings.json](./game-store/appsettings.json) file. 
Also here performed adding middleware components and mapping routing templates, populating the database with initial data.

