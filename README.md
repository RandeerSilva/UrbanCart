
# Effective TDD for C# Applications with External Dependencies

This repository demonstrates a practical approach to Test-Driven Development (TDD) in C# applications that interact with external dependencies, such as Active Directory (AD).
By employing interfaces and mocking, we can isolate external systems to facilitate unit testing using NUnit.

## Overview

The project focuses on an `AuthenticationHandler` class responsible for:

- Validating user credentials against Active Directory.
- Retrieving user details.
- Updating or adding user information in the repository.

To enable effective unit testing, external dependencies are abstracted through interfaces, allowing the use of mocks during testing.

## Technologies Used

- .NET 6.0
- NUnit
- Moq
- AutoMapper

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/RandeerSilva/UrbanCart.git
   ```

2. Navigate to the project directory:

   ```bash
   cd UrbanCart
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

### Running Tests

Execute the following command to run the unit tests:

```bash
dotnet test
```

## Project Structure

- `AuthenticationHandler.cs` - Contains the main authentication logic.
- `IUserRepository.cs` - Interface for user data operations.
- `IPrincipalContextWrapper.cs` - Interface to abstract Active Directory interactions.
- `AuthenticationHandlerTests.cs` - NUnit test cases for `AuthenticationHandler`.

## Key Concepts

### Dependency Injection

External dependencies, such as Active Directory, are abstracted using interfaces (`IPrincipalContextWrapper`). This allows for injecting mock implementations during testing, ensuring that unit tests remain isolated and do not depend on external systems.

### Test Cases

The following scenarios are covered:

1. **Login Failure**: Ensures that the method returns `false` when authentication fails.
2. **Existing User Update**: Verifies that existing user details are updated upon successful authentication.
3. **New User Addition**: Checks that a new user is added to the repository when authenticated and not already present.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

Inspired by Randeer Silva's article: [Effective TDD for C# Applications with External Dependencies: A Practical Guide Using NUnit](https://medium.com/@randeersilva/effective-tdd-for-c-applications-with-external-dependencies-a-practical-guide-using-nunit-cd0765267dc2).
