namespace Data.Repositories;

using Data.Entities;

public class ModelsRepository(AppDatabase db)
{
    public async Task<List<Models>> GetAllAsync()
    {
        var conn = await db.GetConnectionAsync();

        return await conn.Table<Models>()
                         .OrderBy(v => v.Name)
                         .ToListAsync();
    }

    public async Task<Models?> GetByIdAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.FindAsync<Models>(id);
    }

    public async Task<int> SaveAsync(Models model)
    {
        var conn = await db.GetConnectionAsync();

        return model.Id == 0
            ? await conn.InsertAsync(model)
            : await conn.UpdateAsync(model);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.DeleteAsync<Models>(id);
    }
}