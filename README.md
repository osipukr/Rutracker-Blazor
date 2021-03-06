# Rutracker

The application was built on the basis of the Blazor-Hosted template, which includes **Server (RESTful API)**, **Client (Blazor)** and **Shared (Сommon between client and server)**.

The project that allows the user to create an account, add or find a torrent file and upload it. And also allows you to exchange messages between users. It's possible to leave your comments under your torrent.


## Project structure

```
Rutracker
│
└─── Client
│   │   Rutracker.Client.BusinessLayer
│   │   Rutracker.Client.BlazorWasm
│   
└─── Server
│   │   Rutracker.Server.DataAccessLayer
│   │   Rutracker.Server.BusinessLayer
│   │   Rutracker.Server.WebApi
│
└─── Shared
    │   Rutracker.Shared.Infrastructure
    │   Rutracker.Shared.Models
```


## Demo and Documentation

You can see an example of a **demo** application here - [Demo](https://rutracker.azurewebsites.net).

Also, you can see the api endpoints **documentation** here - [Documentation](https://osipukr.github.io/Rutracker-Blazor).


## Running the sample using Docker

You can run the application sample by running these commands from the root folder (where the **.sln** file is located):

```
    docker-compose build
    docker-compose up
```

You should be able to make requests to **localhost:5106** once these commands complete.

You can also run the application by using the instructions located in its `Dockerfile` file in the root of the project. Again, run these commands from the root of the solution (where the **.sln** file is located).
