# Dr. Sillystringz's Factory

This is a web application for managing engineers and machines for a (fictional) factory.

#### By Michael G Connelly

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server 
- Razor pages
- C#
- HTML, CSS, and JavaScript

## Description

This project was created as my fourth independent project, "Many-to-Many Relationships", in the C# / .Net course at Epicodus. It demonstrates my understanding of building an MVC app and connecting it to a database with many-to-many relationships using Entity Framework Core.

## Setup and Installation

Follow these steps to set up and run the Hair Salon web application locally.

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet) installed on your machine.
- SQL Server (or another compatible database) installed.

### Clone the Repository

```bash
git clone https://github.com/Mikerophonic/Factory.Solution.git
cd Factory
```

### Setup database

Create a new file named `appsettings.json` in the project's root directory:

```bash
   $ touch appsettings.json
```

Open the appsettings.json file and insert the following code. Replace [YOUR-USERNAME] and [YOUR-MYSQL-PASSWORD] with your MySQL username and password:

```bash
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=your_database_name;uid=[YOUR-USERNAME];pwd=[YOUR-MYSQL-PASSWORD];"
    }
```
Navigate to the project directory:
```bash
    $ cd Factory
```

<b>Create Database:</b> 
```
$ dotnet ef migrations add Initial
$ dotnet ef database update
```

Start the project in development mode with a watcher using the following command:
```bash
    $ dotnet watch run
```

## Known Bugs

No known bugs.


## License
MIT
Copyright (c) 2023 Michael G Connelly





