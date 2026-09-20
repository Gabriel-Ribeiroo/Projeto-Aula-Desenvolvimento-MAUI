namespace Data.Entities;

using SQLite;

[Table("vehicles")]
public class Vehicles
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int ManufecturerId { get; set; }

    [Indexed]
    public int ModelId { get; set; }

    [MaxLength(50), NotNull, Unique]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [NotNull]
    public int ManufectureYear { get; set; }

    [NotNull]
    public int ModelYear { get; set; }

}