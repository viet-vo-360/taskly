# Taskly  

Taskly is a simple task management web application designed to help users create and track their tasks. This project is built with .NET Core and serves as a hands-on learning experience for working with web technologies and backend development.  

## Features  
- User authentication (register, login)  
- Task creation with detailed information  
- Task progress tracking  
- Responsive UI with modern web technologies  

## Tech Stack  
### Frontend  
- HTML, CSS, JavaScript  
- [SCSS (Sass)](https://sass-lang.com/) for styling
- [JestJs](https://jestjs.io/) for javascript unit testing

### Backend  
- [.NET Core MVC](https://learn.microsoft.com/vi-vn/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-9.0&tabs=visual-studio)  
- [.NET Core API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio)  
- [Xunit](https://xunit.net/) for unit testing

### Database  
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server)  
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)  
- [Liquibase](https://www.liquibase.com/) for database migrations  

### Messaging  
- [RabbitMQ](https://www.rabbitmq.com/)    

## Setup Instructions  
1. Clone the repository:  
   ```sh
   git clone https://github.com/your-repo/taskly.git
   cd taskly
   
2. Configure the database connection in appsettings.json.
3. Run database migrations using Liquibase.
4. Start the application:
   ```sh
   dotnet run

## Contribution
This project is intended for learning purposes. Feel free to contribute by improving the features or optimizing the code.

## License
MIT License


## Config User Secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_NEW_SERVER;Database=Taskly;Trusted_Connection=True;TrustServerCertificate=True;"
