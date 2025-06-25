# 🗃️ SQL Server Column Data Type Retriever

This C# console application retrieves detailed data type information for all columns in a SQL Server table using ADO.NET. It provides a simple command-line interface to connect to your SQL Server instance and inspect table schemas.

## 🚀 Features

- 🔍 Retrieve precise SQL Server column data types
- 🛠 Handle special data types with parameters:
  - 📏 String types with length (CHAR, VARCHAR, NCHAR, NVARCHAR)
  - 🔢 Decimal types with precision/scale (DECIMAL, NUMERIC)
  - ⏱ Time-based types with precision (DATETIME2, TIME, DATETIMEOFFSET)
- 🔐 Support for both Windows Authentication and SQL Server Authentication
- 🔄 Case-insensitive column name handling
- 🛡️ Secure connection handling with proper resource disposal
- 🚨 Comprehensive error handling

## ⚙️ Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- SQL Server instance (2008 or later)
- 🔑 Appropriate permissions to access target databases

## 🖥 How to Use

1. Clone the repository:
   ```bash
   git clone https://github.com/MohmdAliMohmd/sql-column-type-retriever.git
   cd sql-column-type-retriever
   ```

2. Build the application:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Follow the interactive prompts:
   ```bash
   SQL Server Column Data Type Retriever
   ======================================
   Enter SQL Server name: localhost\SQLEXPRESS
   Enter database name: AdventureWorks
   Enter authentication method (1 for Windows, 2 for SQL Server): 1
   Enter table name: Person.Address
   ```

## 📊 Example Output

```
Column Data Types:
------------------
AddressID: INT
AddressLine1: NVARCHAR(60)
AddressLine2: NVARCHAR(60)
City: NVARCHAR(30)
StateProvinceID: INT
PostalCode: NVARCHAR(15)
SpatialLocation: GEOMETRY
rowguid: UNIQUEIDENTIFIER
ModifiedDate: DATETIME
```

## 🧱 Code Structure

### Main Components:

1. **Main Method**:
   - Handles user input for connection details
   - Constructs secure connection strings
   - Calls type retrieval method
   - Displays results

2. **Core Retrieval Method**:
   ```csharp
   static Dictionary<string, string> GetColumnDataTypes(
       string connectionString, 
       string tableName)
   {
       var columnInfo = new Dictionary<string, string>(
           StringComparer.OrdinalIgnoreCase);
       
       using (SqlConnection connection = new SqlConnection(connectionString))
       {
           connection.Open();
           DataTable schemaTable = connection.GetSchema("Columns", 
               new[] { null, null, tableName, null });
           
           foreach (DataRow row in schemaTable.Rows)
           {
               // Column metadata processing
               // Special type handling logic
           }
       }
       return columnInfo;
   }
   ```

## 📋 Supported Data Types

| Data Type Category | Examples                  | Format               |
|--------------------|---------------------------|----------------------|
| String Types       | CHAR, VARCHAR, NCHAR      | TYPE(Length)         |
| MAX Types          | VARCHAR(MAX)              | TYPE(MAX)            |
| Numeric Types      | DECIMAL, NUMERIC          | TYPE(Precision,Scale)|
| Temporal Types     | DATETIME2, TIME           | TYPE(Precision)      |
| Other Types        | INT, UNIQUEIDENTIFIER     | TYPE                 |

## 🔒 Security Notes

- 🔑 Passwords are never stored or displayed
- 💾 Connection strings exist only in memory
- 🛡️ Uses schema retrieval instead of dynamic SQL
- ♻️ Proper resource disposal with `using` statements
- 🚫 No external dependencies beyond .NET BCL

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a new feature branch (`git checkout -b feature/improvement-name`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/improvement-name`)
5. Create a new Pull Request

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

**Note**: This application only retrieves schema metadata and does not access or modify table data. Always ensure you have proper permissions before accessing database schemas.
