namespace TuberTreats.Models.DTOs;
public class TuberOrderDTO
{
    public int Id { get; set; }
    public DateOnly OrderPlacedOnDate { get; set; }
    public int CustomerId { get; set; }
    public int? TuberDriverId { get; set; }
    public DateOnly DeliveredOnDate { get; set; }
    public List<Topping> Toppings { get; set; }
}