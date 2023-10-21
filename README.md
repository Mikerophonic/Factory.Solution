# Dr. Sillystrinz's Factory

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
git clone https://github.com/your-username/HairSalon.git
cd Factorygit
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

<b>Create Database:</b> Follow the instructions provided in the LearnHowToProgram.com lesson titled <a href="https://www.learnhowtoprogram.com/c-and-net/database-basics/creating-a-test-database-exporting-and-importing-databases-with-mysql-workbench">"Creating a Test Database: Exporting and Importing Databases with MySQL Workbench"</a>. Use the michael_connelly.sql file located in the project's root directory to create a new database in MySQL Workbench with the desired database name.

Navigate to the project directory:
```bash
    $ cd HairSalon
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





