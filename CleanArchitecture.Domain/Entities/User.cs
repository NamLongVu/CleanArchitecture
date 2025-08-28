namespace CleanArchitecture.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}