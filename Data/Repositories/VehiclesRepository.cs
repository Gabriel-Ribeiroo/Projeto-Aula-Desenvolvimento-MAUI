namespace Data.Repositories;

using Data.Entities; 

public class VehiclesRepository(AppDatabase db) 
{
    public async Task<List<Vehicles>> GetAllAsync()
    {
        var conn = await db.GetConnectionAsync();

        return await conn.Table<Vehicles>()
                         .OrderBy(v => v.Name)
                         .ToListAsync();
    }

    public async Task<Vehicles?> GetByIdAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.FindAsync<Vehicles>(id);  
    }

    public async Task<int> SaveAsync(Vehicles vehicle)
    {
        var conn = await db.GetConnectionAsync();

        return vehicle.Id == 0
            ? await conn.InsertAsync(vehicle)
            : await conn.UpdateAsync(vehicle);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var conn = await db.GetConnectionAsync();
        return await conn.DeleteAsync<Vehicles>(id);
    }
}