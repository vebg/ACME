# ACME
ACME office time record, this application consume a .txt file with names and day-hours when a employee was in the office and validate with the others employees if there where there have coincidents in the hours.

# Architecture overview
I diside to implement N-Tier architecture - Clean , Addacting this to the current problem and handles the limitation to only use the libraries that a console app give, 
    There are two folder one for locate the project or project if in the furture we will have more [SRC] folder,
    and other one for testing for each project in [SRC], insite src there a layer to storage all the implementations of bussiness logic,(bussiness rules),a layer console.app this is the presentation, Persistence this is where we have our entities and dtos, (if we were to use a database) here will be the migrations and things to configure the database.
    
    Other layer is ACME.Services store only the interfaces to be implemented.
    
    In the tests folder there will be a test project for each project to be testing.
    
    -src
        - ACME.Business.Logic
            - Services
                - Implementations
        - ACME.ConsoleApp
            - Inputs
        - ACME.Persistence
            - Entities
            - Responses
        - ACME.Services
            - Interfaces
    -tests
        - ACME.Business.Logic.Tests.Unit
            - inputs

    
# How I handle the problem ? 




## How to run the project ?
Insite the project folder open the cmd and run 
Example: ![image](https://user-images.githubusercontent.com/9616466/148324232-91e12d4d-90c7-4433-901e-3b1d172d2894.png)
```bash
dotnet run --project ACME.ConsoleApp
```

When you see this in the console write the name of the file with the extension. 
![image](https://user-images.githubusercontent.com/9616466/148326153-5b018174-3be8-4b38-b46a-f39c8182f793.png)
Then 
![image](https://user-images.githubusercontent.com/9616466/148326929-cf09e0e3-42ee-4c83-aa33-0f4f647d03f7.png)


Then you will be the results: 


![image](https://user-images.githubusercontent.com/9616466/148326974-c86f7ebe-84db-4374-a713-9abade952027.png)


# if you need to add other test you only need to add a new file.txt on the inputs folder and the run the application. 


This will restord any dependency, build the project and run the application.

## How to run the tests ?
Insite the project folder open the cmd and run 
Example: ![image](https://user-images.githubusercontent.com/9616466/148324232-91e12d4d-90c7-4433-901e-3b1d172d2894.png)
```bash
dotnet test
```


# Project technical details.
- .NET 5.0.
- Console application.
- Unit test with xUnit.
- Not library installed.
- Outpur .exe.
- Peudo DI

## License
[MIT](https://choosealicense.com/licenses/mit/)
