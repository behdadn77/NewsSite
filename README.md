# NewsSite Project

Welcome to the **NewsSite** project! This is a blog-style website built with ASP.NET Core MVC. It allows users to browse, create, and manage blog articles while supporting multiple user roles such as admins, bloggers, and readers. The project includes features like user authentication, role-based access control, dynamic content rendering, and real-time chat functionality.

---

## Table of Contents

1. [Features](#features)  
2. [Technologies Used](#technologies-used)  
3. [Project Structure](#project-structure)  
4. [Controllers Overview](#controllers-overview)  
5. [Views Overview](#views-overview)  
6. [Getting Started](#getting-started)  
   - [Clone the Repository](#clone-the-repository)  
   - [Configuration](#configuration)  
   - [Database Setup](#database-setup)  
7. [Running the Application](#running-the-application)  
8. [User Roles and Policies](#user-roles-and-policies)  
9. [Contributing](#contributing)  
10. [License](#license)  

---

## Features

- **Article Management**:  
  - View articles with pagination and filtering by categories.  
  - Create, edit, and delete articles (restricted to bloggers).  
  - Article details include comments, author information, and total views.  

- **User Authentication and Authorization**:  
  - Role-based access control using ASP.NET Core Identity.  
  - Admins can manage users and moderate content.  
  - Bloggers can manage their own articles.  
  - Readers can browse articles and post comments.  

- **Dynamic Content**:  
  - Categories are dynamically loaded via AJAX for article creation/editing.  
  - Real-time chat functionality using SignalR.  

- **Responsive Design**:  
  - Fully responsive layout using Bootstrap and custom CSS.  

---

## Technologies Used

- **Backend**: ASP.NET Core MVC, Entity Framework Core, ASP.NET Core Identity  
- **Frontend**: Bootstrap, jQuery, custom CSS  
- **Database**: Microsoft SQL Server (or any EF Core-compatible database)  
- **Real-Time Communication**: SignalR  

---

## Project Structure

The project is organized into the following key directories:

- **Controllers**: Contains the logic for handling HTTP requests and returning responses.  
- **Views**: Contains Razor views for rendering the UI.  
- **Models**: Contains the data models and view models used throughout the application.  
- **Areas**:  
  - **Admins**: Contains controllers and views for admin-specific functionality.  
  - **Bloggers**: Contains controllers and views for blogger-specific functionality.  
  - **Identity**: Contains identity-related pages and configuration.  
- **Migrations**: Contains Entity Framework Core migration files for database schema changes.  
- **wwwroot**: Contains static files such as CSS, JavaScript, and images.  

---

## Controllers Overview

### `HomeController`
- Handles the homepage, article details, and contact page.  
- Key Actions:  
  - `Index`: Displays a list of articles with pagination and category filtering.  
  - `Article`: Displays the details of a specific article.  
  - `Contact`: Displays the contact page.  

### `AccountController`
- Manages user authentication and registration.  
- Key Actions:  
  - `Login`: Displays the login page and handles user login.  
  - `SignUp`: Displays the registration page and handles user registration.  
  - `LogOut`: Logs out the current user.  

### `CommentController`
- Handles user comments on articles.  
- Key Actions:  
  - `PostCommentAsync`: Allows users to post comments on articles.  
  - `DeleteCommentAsync`: Allows users or admins to delete comments.  

### `PostController` (Bloggers Area)
- Manages article creation, editing, and deletion for bloggers.  
- Key Actions:  
  - `AddArticle`: Displays the form for creating a new article.  
  - `EditArticle`: Displays the form for editing an existing article.  
  - `DeleteArticle`: Deletes an article.  

### `ManageUsersController` (Admins Area)
- Manages user roles and permissions.  
- Key Actions:  
  - `Index`: Displays a list of users and their roles.  
  - `ChangeUserInRolesAsync`: Updates user roles dynamically.  

### `ChatRoomController`
- Manages the real-time chat room functionality.  
- Key Actions:  
  - `Index`: Displays the chat room interface.  

---

## Views Overview

### Shared Views
- `_Layout.cshtml`: The main layout file used across the application.  
- `_LoginPartial.cshtml`: Displays login/logout links based on user authentication status.  

### Home Views
- `Index.cshtml`: Displays the list of articles on the homepage.  
- `Article.cshtml`: Displays the details of a specific article.  
- `Contact.cshtml`: Displays the contact page with an embedded Google Map.  

### Account Views
- `Login.cshtml`: Displays the login form.  
- `SignUp.cshtml`: Displays the registration form.  

### Bloggers Area Views
- `AddArticle.cshtml`: Displays the form for creating a new article.  
- `EditArticle.cshtml`: Displays the form for editing an existing article.  

### Admins Area Views
- `ManageUsers/Index.cshtml`: Displays the user management interface for admins.  

---

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/behdadn77/NewsSite.git
cd NewsSite
### Compile the Project

To compile the project, follow these steps:

1. Ensure you have the following prerequisites installed:
    - [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later).
    - A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

2. Open a terminal or command prompt in the project directory.

3. Run the following command to restore dependencies and build the project:

    ```bash
    dotnet build
    ```

4. If the build succeeds, the compiled binaries will be available in the `bin/Debug/net6.0` directory (or a similar path based on your .NET version).

5. You can now proceed to configure and run the application.