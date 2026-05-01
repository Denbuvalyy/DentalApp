namespace DentalApp.Core.Models;

public class Visit
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}