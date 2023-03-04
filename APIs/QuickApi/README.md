## Shopping list application with ASP.NET Core 

This application is a basic example of modern Web Api development.

It showcases:
- Minimal APIs
- Using EntityFramework
- In memory database
- OpenAPI
- Docker

## Prerequisites

### .NET
1. [Install .NET 7](https://dotnet.microsoft.com/en-us/download)

### Running the application

To run the Api you can use one of the following options:
* **Visual Studio or Rider** - Select QuickApi as starting project and click Run or Debug.
* **Terminal or CMD** - Open your console or terminal and navigate to [QuickApi](QuickApi) folder and run:
  ```
  dotnet run -lp http
  ```
  This will start the application with the `https` profile.

* **Docker Compose** - Open your console or terminal, navigate to the root folder of this project and run the following commands:

1. Open your console or terminal and navigate to [QuickApi](QuickApi).
2. Build a docker image:
    ```
    docker build -t quick-api .
    ```
3. Run container
    ```
    docker run -d --name quick-api -p 5017:80 quick-api 
    ```
4. Navigate to [Swagger](http://localhost:5017/swagger/index.html)
