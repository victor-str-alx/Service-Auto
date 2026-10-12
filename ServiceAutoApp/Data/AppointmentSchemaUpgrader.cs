using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ServiceAutoApp.Data;

public static class AppointmentSchemaUpgrader
{
    public static async Task EnsureTimeSlotColumnAsync(ApplicationDbContext context)
    {
        await using var connection = context.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var checkCmd = connection.CreateCommand();
        checkCmd.CommandText = "PRAGMA table_info('Appointments');";
        await using var reader = await checkCmd.ExecuteReaderAsync();

        var hasTimeSlot = false;
        while (await reader.ReadAsync())
        {
            if (string.Equals(reader.GetString(1), "TimeSlot", StringComparison.OrdinalIgnoreCase))
            {
                hasTimeSlot = true;
                break;
            }
        }

        if (!hasTimeSlot)
        {
            await using var alterCmd = connection.CreateCommand();
            alterCmd.CommandText = "ALTER TABLE Appointments ADD COLUMN TimeSlot TEXT NOT NULL DEFAULT '';";
            await alterCmd.ExecuteNonQueryAsync();
        }
    }
}
