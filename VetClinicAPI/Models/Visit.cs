namespace VetClinicAPI.Models;

public class Visit
{
    public int Id { get; set; }
    public DateTime DateOfVisit { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int AnimalId { get; set; }
}

