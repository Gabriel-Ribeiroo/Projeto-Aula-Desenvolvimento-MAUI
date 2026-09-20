namespace Data.Entities;

using SQLite;

[Table("models")]
public class Models
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(50), NotNull, Unique]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

}