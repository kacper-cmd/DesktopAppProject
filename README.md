## Built With

1. Visual Studio 2022
2. NET 8.0 SDK
3. SQL Server (LocalDb)

## About the Project

MusicApp is a **\*WPF** application built with .NET 8.0 and C# 12.0. It is designed to manage music-related data, including albums, artists, songs, playlists, podcasts, and more. The application uses Entity Framework Core for data access and CommunityToolkit.Mvvm for MVVM support.

## Getting started

- **Clone Repository** git clone https://github.com/kacper-cmd/DesktopAppProject.git cd DesktopAppProject
- **Update DB Connection String**

````csharp
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("YourConnectionStringHere");
 ```

##Project Structure

•	Models: Contains the data models and the DatabaseContext class for Entity Framework Core.
•	ViewModels: Contains the view models for the MVVM architecture.
•	Views: Contains the WPF views and resources.
•	Helpers: Contains helper classes and utilities.
•	BusinessLogic: Contains business logic classes.

## Contributing

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Contact

### Would you like to any notify or suggest something to me?

kacper.obrzut1@gamil.com
````
