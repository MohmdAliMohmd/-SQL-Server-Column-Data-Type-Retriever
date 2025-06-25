using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SqlColumnTypeInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SQL Server Column Data Type Retriever");
            Console.WriteLine("======================================");

            // Get connection details from user
            Console.Write("Enter SQL Server name: ");
            string server = Console.ReadLine();

            Console.Write("Enter database name: ");
            string database = Console.ReadLine();

            Console.Write("Enter authentication method (1 for Windows, 2 for SQL Server): ");
            int authChoice = Convert.ToInt32(Console.ReadLine());

            string connectionString;
            if (authChoice == 1)
            {
                connectionString = $"Server={server};Database={database};Integrated Security=True;";
            }
            else
            {
                Console.Write("Enter username: ");
                string userId = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                connectionString = $"Server={server};Database={database};User Id={userId};Password={password};";
            }

            Console.Write("Enter table name: ");
            string tableName = Console.ReadLine();

            try
            {
                // Retrieve column information
                Dictionary<string, string> columnTypes = GetColumnDataTypes(connectionString, tableName);

                // Display results
                Console.WriteLine("\nColumn Data Types:");
                Console.WriteLine("------------------");
                foreach (var column in columnTypes)
                {
                    Console.WriteLine($"{column.Key}: {column.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static Dictionary<string, string> GetColumnDataTypes(string connectionString, string tableName)
        {
            var columnInfo = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use schema retrieval instead of direct query
                DataTable schemaTable = connection.GetSchema("Columns", new[] { null, null, tableName, null });

                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = row["COLUMN_NAME"].ToString();
                    string dataType = row["DATA_TYPE"].ToString();

                    // Handle special types with parameters
                    switch (dataType.ToUpper())
                    {
                        case "VARCHAR":
                        case "CHAR":
                        case "NVARCHAR":
                        case "NCHAR":
                        case "BINARY":
                        case "VARBINARY":
                            int maxLength = Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]);
                            columnInfo[columnName] = maxLength == -1 ?
                                $"{dataType}(MAX)" : $"{dataType}({maxLength})";
                            break;

                        case "DECIMAL":
                        case "NUMERIC":
                            byte precision = Convert.ToByte(row["NUMERIC_PRECISION"]);
                            byte scale = Convert.ToByte(row["NUMERIC_SCALE"]);
                            columnInfo[columnName] = $"{dataType}({precision},{scale})";
                            break;

                        case "DATETIME2":
                        case "TIME":
                        case "DATETIMEOFFSET":
                            byte timePrecision = Convert.ToByte(row["DATETIME_PRECISION"]);
                            columnInfo[columnName] = $"{dataType}({timePrecision})";
                            break;

                        default:
                            columnInfo[columnName] = dataType;
                            break;
                    }
                }
            }

            return columnInfo;
        }
    }
}