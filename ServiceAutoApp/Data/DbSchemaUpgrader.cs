using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ServiceAutoApp.Data;

public static class DbSchemaUpgrader
{
    public static async Task EnsureCustomerPasswordHashColumnAsync(ApplicationDbContext context)
    {
        await using var connection = context.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var checkCmd = connection.CreateCommand();
        checkCmd.CommandText = "PRAGMA table_info('Customers');";
        await using var reader = await checkCmd.ExecuteReaderAsync();

        var hasPasswordHash = false;
        while (await reader.ReadAsync())
        {
            var columnName = reader.GetString(1);
            if (string.Equals(columnName, "PasswordHash", StringComparison.OrdinalIgnoreCase))
            {
                hasPasswordHash = true;
                break;
            }
        }

        if (hasPasswordHash)
        {
            return;
        }

        await using var alterCmd = connection.CreateCommand();
        alterCmd.CommandText = "ALTER TABLE Customers ADD COLUMN PasswordHash TEXT NULL;";
        await alterCmd.ExecuteNonQueryAsync();
    }
}
