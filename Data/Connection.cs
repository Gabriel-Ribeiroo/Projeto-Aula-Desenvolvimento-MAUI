namespace Data;

using Data.Entities;

using SQLite;

public class AppDatabase(string dbPath)
{
    private readonly SemaphoreSlim gate = new(1, 1);
    private SQLiteAsyncConnection? connection;

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (connection is not null)
            return connection;

        await gate.WaitAsync();
        try
        {
            if (connection is null)
            {
                var conn = new SQLiteAsyncConnection(
                    dbPath,
                    SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

                await conn.ExecuteAsync("PRAGMA foreign_keys = ON;");

                await conn.CreateTableAsync<Manufacturers>();
                await conn.CreateTableAsync<Models>();
                await conn.CreateTableAsync<Vehicles>();

                connection = conn;
            }
        }
        finally
        {
            gate.Release();
        }

        return connection;
    }
}