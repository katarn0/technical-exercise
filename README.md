# User Management API

This project is an ASP.NET Core Web API that manages users, providing standard CRUD operations. The API adheres to RESTful principles and uses Entity Framework Core for data persistence.

---

## Features

- **Endpoints**:
  - `POST /api/users`: Create a new user.
  - `GET /api/users`: Retrieve a list of all users.
  - `GET /api/users/{id}`: Retrieve a specific user by ID.
  - `PUT /api/users/{id}`: Update an existing user.
  - `DELETE /api/users/{id}`: Delete a user by ID.
- **Business Rules**:
  - Users must have a unique email address.
  - Users must be 18 years or older.
  - Users include additional calculated fields like `Age`.
- **Validation**:
  - `FirstName`, `Email`, `PhoneNumber`, and `DateOfBirth` are required.
  - Email must be valid.
  - Phone numbers must be 10 digits.
  - Maximum length of `FirstName` and `LastName` is 128 characters.

---

## Technologies Used

- **.NET Core 8**: Framework for building the API.
- **C# 12**: Programming language.
- **Entity Framework Core**: ORM for database operations.
- **Serilog**: For logging operations and exceptions.
- **nUnit**: For unit testing.

---

## Best Practices Implemented

- **SOLID Principles**:
  - Separation of concerns by using services (`IUserService`, `UserService`) and controllers (`UsersController`).
  - Dependency injection used for service decoupling.
- **Validation**:
  - FluentValidation used for Age checking.
  - Data annotations used to ensure proper user input.
  - Unique email enforced through database constraints.
- **Logging**:
  - Serilog added to log every operation and exception for easier debugging.
- **Clean Code**:
  - Code is structured and modular, making it easy to maintain and extend.
- **Testability**:
  - Interfaces (`IUserService`) are used to abstract business logic for unit testing.
  - Separate database model (`User`) and domain model (`UserDto`, `CreateUserDto`, and `UpdateUserDto`).

---

## Project Structure

- **Controllers**: Contains the `UsersController` for handling API requests.
- **Services**: Contains `IUserService` and `UserService` for business logic.
- **Models**: Contains `UserDto`, `CreateUserDto`, and `UpdateUserDto` (domain model).
- **Data**:
  - `UserContext`: EF Core database context.
  - `User`: Database model.
- **Tests**: Unit tests written with `nUnit`.

---

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone <repository_url>
   ```
2. Run the Database Creation script from under the Database folder.
3. Navigate to the project directory:
   ```bash
   cd UserApi
   ```
4. Build the project:
   ```bash
   dotnet build
   ```
5. Run the API:
   ```bash
   dotnet run
   ```
6. The API will be available at `http://localhost:5079` (or a different port specified in `launchSettings.json`).

---

## Testing

1. Run the tests:
   ```bash
   dotnet test
   ```

---

## Architectural Style

- **Layered Architecture**:
  - **Controller Layer**: Handles HTTP requests and responses.
  - **Service Layer**: Contains business logic and handles data operations.
  - **Data Layer**: Handles interaction with the database using EF Core.

### Why Layered Architecture?

This style separates concerns, making the code more maintainable, testable, and scalable.

---

## License

There is no license, this is for a Technical Exercise only.