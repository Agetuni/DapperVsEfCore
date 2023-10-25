# Dapper vs. Entity Framework Performance Comparison

This sample project, **DapperVsEntityFramework**, is a .NET 7 console application that demonstrates the difference in performance between two popular data access technologies: Dapper and Entity Framework. This project is intended to help you understand the advantages and disadvantages of each technology when it comes to working with databases.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Running the Application](#running-the-application)
- [Performance Comparison](#performance-comparison)
- [Conclusion](#conclusion)

## Introduction

Dapper and Entity Framework are both Object-Relational Mapping (ORM) frameworks for .NET that simplify database interactions. However, they have different design philosophies and performance characteristics. This project aims to compare the performance of these two technologies in a realistic scenario.

## Prerequisites

Before running the project, make sure you have the following prerequisites installed on your system:

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- A SQL Server instance (LocalDB or another) with the sample database (included in the project).

## Getting Started

1. Clone this repository to your local machine:

    ```bash
    git clone https://github.com/yourusername/DapperVsEntityFramework.git
    ```

2. Navigate to the project directory:

    ```bash
    cd DapperVsEntityFramework
    ```

## Project Structure

The project structure is organized as follows:

- `DaperVsEntityFramework/Program.cs`: The main entry point of the application.
- `DaperVsEntityFramework/Data`: Contains database context and data access code.
- `DaperVsEntityFramework/Models`: Defines the data models used in the application.

## Running the Application

1. Open the project in your preferred .NET IDE (e.g., Visual Studio, Visual Studio Code).
2. Build the project to restore dependencies.
3. Modify the database connection string in the `appsettings.json` file to match your SQL Server setup.

```json
"ConnectionStrings": {
  "DefaultConnection": "YourConnectionStringHere"
}
```

4. Open the terminal or command prompt, navigate to the project directory, and run the application:

```bash
dotnet run
```

The application will execute database operations using both Dapper and Entity Framework and display the execution times.

## Performance Comparison

The application performs the following database operations for each technology:

1. **Select All**: Retrieves all records from a sample table.
2. **Select Single**: Retrieves a single record from the sample table.
3. **Insert**: Inserts a new record into the sample table.
4. **Update**: Updates an existing record in the sample table.
5. **Delete**: Deletes a record from the sample table.

The execution times for these operations will be displayed in the console, allowing you to compare the performance of Dapper and Entity Framework.

## Conclusion

This project provides a simple yet practical example of the performance differences between Dapper and Entity Framework. After running the application, you should have a better understanding of when to use each technology based on your specific application requirements.

Feel free to explore the code and experiment with different scenarios to gain further insights into database access performance.
