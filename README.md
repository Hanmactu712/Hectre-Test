## Hectre-Test

This is test project. To run the project, it requires Docker to be installed first.

This project contains:

- Persistence: MySQL
- Back-end: .NET 5 with Graphql API
- Front-end: React

## How to run

### Run via Docker

1. Download or checkout the project.
2. Open the terminal or window powershell and navigate to the project folder.
3. Run command:

```
docker-compose up
```

4. Or run the bat file ***start_app.bat*** (under administrator right) in the root folder of the project.
5. Access to the application via link: "http://localhost:3000/".
6. To clear all docker image created while running application. Run the bat file ***stop_app.bat*** (under administrator right).

### If there is no docker installed, following these step to run the application

1. Download or checkout the project.
2. Open the terminal or window powershell and navigate to the project folder.
3. Install the MySQL server and create a database schema named: ***hectre_db***. Note down the corresponding connection string.
4. Navigate to backend backend folder: ./Backend/Hectre.BackEnd/Hectre.BackEnd.
5. Looking for and ppen the appSetting.json file, update the value of ConnectionString to point the the installed mySql database above.

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "ConnectionStrings": {
    "DemoMySqlConnection": "server=localhost;uid=root;pwd=abcde12345-;database=hectre_db"
  },

  "AllowedHosts": "*"
}
```

6. Run the following command:

```
dotnet restore
dotnet run
```

7. note down the url of the backend application, the default is "https://localhost:5001" or "http://localhost:5000".
8. Navigate the the front-end folder: ./Frontend/hectre-Frontend.
9. Looking for and open file: configuration.ts file following the path ./src/common/configuration.ts and update the value of key ***GraphQlApiUrl*** to ***{{backend-url}}/graphql***. for example: https://localhost:5001/graphql or http://localhost:5000/graphql

```
export const Configuration = {
  GraphQlApiUrl: "http://localhost:5000/graphql",
};
```

10. Run the following command:

```
npm install
npm start
```

11. Access to the application via link: "http://localhost:3000/"
