using TuberTreats.Models;
using TuberTreats.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//add endpoints here
List<TuberDriver> tuberDrivers = new List<TuberDriver>
{
    new TuberDriver
    {
        Id = 1,
        Name = "Maya Rodriguez"
    },
    new TuberDriver
    {
        Id = 2,
        Name = "Ethan Brooks"
    },
    new TuberDriver
    {
        Id = 3,
        Name = "Jordan Lee"
    }
};
List<Customer> customers = new List<Customer>
{
    new Customer
    {
        Id = 1,
        Name = "Sarah Johnson",
        Address = "123 Maple Street"
    },
    new Customer
    {
        Id = 2,
        Name = "Marcus Williams",
        Address = "456 Oak Avenue"
    },
    new Customer
    {
        Id = 3,
        Name = "Emily Carter",
        Address = "789 Pine Road"
    },
    new Customer
    {
        Id = 4,
        Name = "David Thompson",
        Address = "321 Cedar Lane"
    },
    new Customer
    {
        Id = 5,
        Name = "Olivia Martinez",
        Address = "654 Birch Boulevard"
    }
};
List<Topping> toppings = new List<Topping>
{
    new Topping
    {
        Id = 1,
        Name = "Cheddar Cheese"
    },
    new Topping
    {
        Id = 2,
        Name = "Sour Cream"
    },
    new Topping
    {
        Id = 3,
        Name = "Chives"
    },
    new Topping
    {
        Id = 4,
        Name = "Bacon"
    },
    new Topping
    {
        Id = 5,
        Name = "Jalapeños"
    }
};
List<TuberOrder> tuberOrders = new List<TuberOrder>
{
    new TuberOrder
    {
        Id = 1,
        OrderPlacedOnDate = new DateOnly(2026, 9, 20),
        CustomerId = 1,
        TuberDriverId = 1,
        DeliveredOnDate = new DateOnly(2026, 9, 20)
    },
    new TuberOrder
    {
        Id = 2,
        OrderPlacedOnDate = new DateOnly(2026, 9, 21),
        CustomerId = 2,
        TuberDriverId = 2,
        DeliveredOnDate = new DateOnly(2026, 9, 21)
    },
    new TuberOrder
    {
        Id = 3,
        OrderPlacedOnDate = new DateOnly(2026, 9, 22),
        CustomerId = 3,
        TuberDriverId = null,
        DeliveredOnDate = new DateOnly(2026, 9, 22)
    }
};
List<TuberTopping> tuberToppings = new List<TuberTopping>
{
    new TuberTopping
    {
        Id = 1,
        TuberOrderId = 1,
        ToppingId = 1
    },
    new TuberTopping
    {
        Id = 2,
        TuberOrderId = 1,
        ToppingId = 4
    },
    new TuberTopping
    {
        Id = 3,
        TuberOrderId = 2,
        ToppingId = 2
    },
    new TuberTopping
    {
        Id = 4,
        TuberOrderId = 2,
        ToppingId = 3
    },
    new TuberTopping
    {
        Id = 5,
        TuberOrderId = 3,
        ToppingId = 5
    }
};

app.Run();
//don't touch or move this!
public partial class Program { }