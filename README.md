# BooksHaven 📚

BooksHaven is a lightweight and user-friendly mobile application built with .NET MAUI. It allows users 
to search for books using the Google Books API, view detailed information about them, and manage a personal library of "read" books.
Perfect for book enthusiasts who want a simple, beautiful way to manage their reading list! 😊

---

## Table of Contents
1. [TL;DR](#tldr)
2. [Features](#features)
3. [Screenshots](#screenshots)
4. [Prerequisites](#prerequisites)
5. [Setup and Installation](#setup-and-installation)
6. [Running the Application](#running-the-application)
7. [Troubleshooting](#troubleshooting)

---

## TL;DR

- Clone the repository.
- Create a `Constants.cs` file for the API key.
- Install dependencies.
- Run the application using your IDE (e.g., Visual Studio, Rider).
- Enjoy managing your library with BooksHaven! 🎉

---

## Features

- 🔍 **Search for Books**: Find books using the Google Books API by title, author, or keywords.  
- 🔄 **Sort & Pagination**: Sort books and navigate through multiple pages of search results seamlessly.  
- 📖 **View Book Details**: Access comprehensive details, including the title, author, description, publication date, and a thumbnail image.  
- 📚 **Personal Library**: Mark books as "Read" and manage your personal library efficiently.  
- 🚀 **Cross-Platform Compatibility**: Enjoy the app on Android, iOS, and Windows devices.  


---

## Screenshots

1. **Search Page**  
 Search for your favorite books in Google Books.  
   ![image](https://github.com/user-attachments/assets/081b1e82-6996-4175-a016-2a1e34769ba2)



2. **Book Details Page**  
   View detailed information about the selected book like Description, Published Date etc.
   ![image](https://github.com/user-attachments/assets/a5b14c60-07a4-410b-aa28-b87b12d6e143)


3. **Library Page**  
   Manage your personal library of read books and store locally on your device.  
   ![image](https://github.com/user-attachments/assets/ce70ddc9-5376-40b7-8b16-6135cf6cec3d)


---

## Prerequisites

Before running the application, ensure the following software is installed on your machine:

1. **.NET MAUI Framework**: Install the .NET MAUI workload using Visual Studio.
   - For Windows, enable the **Mobile development with .NET** workload in Visual Studio.
   - For macOS, install Visual Studio with **.NET MAUI** support.
2. **Google Books API Key**: Generate an API key from the [Google Cloud Console](https://console.cloud.google.com/).

---

## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/bookshaven.git
   cd bookshaven
2. Create the Constants.cs file:
   This file shoulld be llocated in the root of the BooksHaven project.
   
    ![image](https://github.com/user-attachments/assets/c717cbe7-672c-4473-965c-6e7e0fbe581e)
   
4. Install depencencies
   If using Visual Studio, open the solution file (BooksHaven.sln) and restore NuGet packages.
   Alternatively, use the command line:
    ```dotnet restore
   
## Running the Application

1. Open the solution file (BooksHaven.sln) in your IDE (e.g., Visual Studio).
2. Select your target platform (e.g., Windows,Android)
3. Build and run the application
4. The Application will launch on your selected platform.

## Troubleshooting

Common Issues:
1. Error: Missing API Key
   Ensure the Constans.cs file exist in the BooksHaven project with the correct API key
2. Build Errors
   Check that all NuGet packages are restored.
   Verify that you have installed the necessary .NET MAUI workloads.
3. UI Not Updating
   Ensure your bindings in the XAML file mathc the properties defined in the corresponding ViewModel.
   
