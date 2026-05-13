using System.ComponentModel.DataAnnotations.Schema;

namespace DentalApp.Core.Models;

public class VisitPhoto
{
    public int Id { get; set; }

    public int VisitId { get; set; }

    public string FilePath { get; set; } // де лежить файл

    public DateTime CreatedAt { get; set; }
}