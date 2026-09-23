namespace TuberTreats.Models.DTOs;
public class ToppingDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TuberOrder> TuberOrders { get; set; }
}