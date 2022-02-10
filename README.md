## Hectre-Test

This is test project. To run the project, it requires Docker to be installed first.

This project contains:

- Persistence: MySQL
- Back-end: .NET 5 API
- Front-end: React application

## How to run

### Run via Docker

1. Download or checkout the project
2. Open the terminal or window powershell and navigate to the project folder.
3. Run command:

```
docker-compose up
```

4. Or run the bash file in the root folder of the project: "start_app.bat"
5. Access to the application via link: "http://localhost:3000/"

### If there is no docker installed, following these step to run the application

1. Download or checkout the project
2. Open the terminal or window powershell and navigate to the project folder.
3. Install the MySQL server and create a database schema named: "hectre_db". Note down the corresponding connection string.
4. Navigate to backend backend folder: ./Backend/Hectre.BackEnd/Hectre.BackEnd
5. Looking for and ppen the appSetting.json file, update the value of ConnectionString to point the the installed mySql database above
6. Run the following command:

```
dotnet restore
dotnet run
```

7. note down the url of the backend application, the default is "https://localhost:5001".
8. Navigate the the front-end folder: ./Frontend/hectre-Frontend
9. Looking for and open file: configuration.ts file following the path ./src/common/configuration.ts and update the value of key "GraphQlApiUrl" to "{{backend-url}}/graphql". for example: https://localhost:5001/graphql
10. Run the following command:

```
npm install
npm start
```

11. Access to the application via link: "http://localhost:3000/"
