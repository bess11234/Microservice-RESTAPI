# Prerequisites

Ensure you have the following installed:
- [.NET SDK 7.0](https://dotnet.microsoft.com/download)
- Any IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/))
- Basic understanding of C# and Entity Framework Core

---

# Installation and Setup

## 1. Clone the Repository
```bash
git clone <repository-url>
cd LawsuitManagement
```

## 2. Restore Dependencies
Run the following command in the root directory to restore all required dependencies:
```bash
dotnet restore
```

## 3. Configure Database
The application uses an **In-Memory Database** by default. You can use other database providers (e.g., SQL Server or PostgreSQL) by modifying the `DbContext` configuration in `Program.cs`:

```csharp
builder.Services.AddDbContext<LawsuitContext>(options =>
    options.UseSqlServer("YourConnectionString"));
```

## 4. Add Default Data
Default data is stored in a JSON file named `seed.json`. Make sure this file is in the root directory or the correct folder as specified in `Program.cs`.

### Example `seed.json`:
```json
[
    {
        "id": 1,
        "title": "Case 101",
        "envident": ["Document1", "Photo1"],
        "prosecutionStatus": "Ongoing"
    },
    {
        "id": 2,
        "title": "Case 202",
        "envident": ["Document2", "Photo2"],
        "prosecutionStatus": "Resolved"
    }
]
```

## 5. Run the Application
To start the application, run the following command:
```bash
dotnet run
```

---

# Customizing Seed Data
- Modify the `seed-data.json` file with new or updated lawsuits.
- Restart the application to re-seed the database (if no data exists).

---

# Testing

You can use tools like **Postman** or **curl** to test the API endpoints.

Example `GET` request to fetch all lawsuits:
```bash
GET http://localhost:5106/api/Lawsuit/
```

---

# Future Improvements
- Implement authentication and authorization.
- Add support for relational data (e.g., lawyers, clients).
- Integrate with a persistent database like SQL Server.
- Add unit and integration tests.

