# Library System Web Application

This web application is designed to manage a library system. It includes features to manage authors, publishers, and books. The application is built using the following technologies:

- ASP.NET Core 8
- Angular 16
- TypeScript
- Entity Framework
- SQL Server
- HTML5
- CSS3
- ng-bootstrap
- ngx-toastr

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Swagger Documentation](#swagger-documentation)
- [Features](#features)
- [Application Architecture ](#ApplicationArchitecture )


## Getting Started

To get a copy of this project up and running on your local machine, follow the steps provided in the [Installation](#installation) section.

## Prerequisites

Make sure you have the following software installed on your machine:

- [Node.js](https://nodejs.org/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/AhmedSeef/LibrarySystem.git

2. cd LibrarySystem 
    ```bash
    cd LibrarySystem

3. Install the necessary npm packages for the Angular frontend:
    ```bash
    cd librarysystem.client
4. Set up the database
Open the appsettings.json file in the LibrarySystem.Server project and configure the connection string for your SQL Server.
Update the connection string as needed.
In the Package Manager Console (PMC) or terminal, run the following commands:
    ```bash
    cd LibrarySystem.Infrastructure
    dotnet ef migrations add InitialCreate
    dotnet ef database update

5. Build the solution to restore NuGet packages
    ```bash
    dotnet build

6. Run the application:
    ```bash
    dotnet run

## Usage
once you run the application you can deal with api endpoints in swagger

## Swagger Documentation
The Swagger documentation for the API can be accessed at /swagger/index.html after running the application. This provides a user-friendly interface to explore and test the available endpoints.


## Features

# BaseEntity Class
public abstract class BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EditedAt { get; set; }
    public bool IsDeleted { get; set; }
}


# Author Class
The Author class inherits from BaseEntity and adds a collection navigation property for books authored by the author.
public class Author : BaseEntity
{
    public Author()
    {
        Books = new HashSet<Book>();
    }

    public virtual ICollection<Book> Books { get; set; }
}


# Book Class
The Book class inherits from BaseEntity and includes additional properties such as AuthorId and PublisherId to establish relationships with the Author and Publisher entities.
public class Book : BaseEntity
{
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }

    public int PublisherId { get; set; }
    public virtual Publisher? Publisher { get; set; }
}


# Publisher Class
The Publisher class inherits from BaseEntity and, similar to the Author class, includes a collection navigation property for books published by the publisher.
public class Publisher : BaseEntity
{
    public Publisher()
    {
        Books = new HashSet<Book>();
    }

    public virtual ICollection<Book> Books { get; set; }
}


## Application Architecture

## Onion Architecture
# Core Domain
The core of the application consists of entities representing the fundamental building blocks: Author, Book, and Publisher. These entities are defined with their properties, relationships, and business logic. They are the heart of the application and are independent of any infrastructure concerns.

# Repository
The Repository pattern is employed to encapsulate the data access logic for each entity. Specific repositories are created for Author, Book, and Publisher, allowing the application to interact with the data without exposing the underlying data store implementation. A generic repository is also introduced to handle common CRUD operations that are shared among entities.

# Unit of Work
The Unit of Work pattern is implemented to manage the transactions across multiple repositories. It ensures that a set of operations is treated as a single unit, either all succeeding or all failing. This pattern promotes consistency and data integrity.

# Data Access Layer
The data access layer is responsible for interacting with the database. Repositories are implemented in this layer, providing a clear and standardized interface for data access.

# Generic Repository
The introduction of a generic repository allows for a common set of methods for CRUD operations to be shared among entities. This promotes code reusability and reduces redundancy in the data access layer.

# CRUD Operations
Each repository provides methods for Create, Read, Update, and Delete (CRUD) operations. These methods are designed to handle the specific requirements of each entity, taking into consideration the relationships between entities.

# Business Logic Layer
The business logic layer contains the core logic of the application, including any business rules or workflows related to the entities. This layer is independent of the data access layer, allowing for easier testing and maintenance.

# Presentation Layer
The presentation layer is responsible for presenting the data to the user and receiving user input. It interacts with the business logic layer to perform operations on the entities.

   

