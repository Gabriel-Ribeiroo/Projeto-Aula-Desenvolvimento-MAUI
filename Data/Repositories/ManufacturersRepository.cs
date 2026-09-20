namespace Data.Repositories;

using Data.Entities;

public class ManufacturersRepository(AppDatabase db)
{
    public async Task<List<Manufacturers>> GetAllAsync()
    {
        var conn = await db.GetConnectionAsync();

        return await conn.Table<Manufacturers>()
                         .OrderBy(v => v.Name)
                         .ToListAsync();
    }

    public async Task<Manufacturers?> GetByIdAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.FindAsync<Manufacturers>(id);
    }

    public async Task<int> SaveAsync(Manufacturers manufacturer)
    {
        var conn = await db.GetConnectionAsync();

        return manufacturer.Id == 0
            ? await conn.InsertAsync(manufacturer)
            : await conn.UpdateAsync(manufacturer);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.DeleteAsync<Manufacturers>(id);
    }
}