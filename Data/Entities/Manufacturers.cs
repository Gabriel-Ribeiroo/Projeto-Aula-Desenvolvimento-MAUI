namespace Data.Entities;

using SQLite;

[Table("manufacturers")]
public class Manufacturers
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(50), NotNull, Unique]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; } 

}