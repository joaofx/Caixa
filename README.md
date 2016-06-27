# Caixa

Caixa is a simple app to register spent and received money in a small company.

It uses Felice Framework (https://github.com/joaofx/felice) and its dependencies: ASP.NET MVC, NHibernate, FluentMigrator, StructureMap, NUnit and others.

### Requirements

It is required PostgreSQL database.

### How To Run

- Clone the source code
- Create a database for development and another for run integrated tests 
- Configure the connection strings in Demo/Web.config and IntegrationTests/App.config
- Run Demo or run the tests. Both projects will create tables automatically
