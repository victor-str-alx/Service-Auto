using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ServiceAutoApp.Data;

public static class DbContextRetryExtensions
{
    public static async Task SaveChangesWithRetryAsync(this DbContext context, CancellationToken cancellationToken = default)
    {
        const int maxAttempts = 3;
        const int delayMs = 200;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                return;
            }
            catch (DbUpdateException ex) when (IsSqliteLock(ex) && attempt < maxAttempts)
            {
                await Task.Delay(delayMs * attempt, cancellationToken);
            }
        }
    }

    private static bool IsSqliteLock(DbUpdateException ex)
    {
        if (ex.InnerException is not SqliteException sqliteEx)
        {
            return false;
        }

        return sqliteEx.SqliteErrorCode is 5 or 6;
    }
}
