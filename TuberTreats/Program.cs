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

//TuberOrders get all
app.MapGet("/tuberOrders", () =>
{
    return tuberOrders.Select(t => new TuberOrderDTO
    {
        Id = t.Id,
        OrderPlacedOnDate = t.OrderPlacedOnDate,
        CustomerId = t.CustomerId,
        TuberDriverId = t.TuberDriverId,
        DeliveredOnDate = t.DeliveredOnDate
    });
});

//TuberOrders get by Id
app.MapGet("/tuberOrders/{id}", (int id) =>
{
    TuberOrder? tuberOrder = tuberOrders.FirstOrDefault(
        to => to.Id == id
    );

    if (tuberOrder == null)
    {
        return Results.NotFound();
    }

    Customer? customer = customers.FirstOrDefault(
        c => c.Id == tuberOrder.CustomerId
    );

    TuberDriver? driver = tuberOrder.TuberDriverId == null
        ? null
        : tuberDrivers.FirstOrDefault(
            d => d.Id == tuberOrder.TuberDriverId
        );

    List<Topping> toppingsForOrder = tuberToppings
        .Where(tt => tt.TuberOrderId == tuberOrder.Id)
        .Select(tt => toppings.First(t => t.Id == tt.ToppingId))
        .ToList();

    return Results.Ok(new TuberOrderDTO
    {
        Id = tuberOrder.Id,
        OrderPlacedOnDate = tuberOrder.OrderPlacedOnDate,

        CustomerId = tuberOrder.CustomerId,
        Customer = new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address
        },

        TuberDriverId = tuberOrder.TuberDriverId,
        TuberDriver = driver == null
            ? null
            : new TuberDriverDTO
            {
                Id = driver.Id,
                Name = driver.Name
            },

        DeliveredOnDate = tuberOrder.DeliveredOnDate,

        Toppings = toppingsForOrder.Select(t => new ToppingDTO
        {
            Id = t.Id,
            Name = t.Name
        }).ToList()
    });
});

//TuberOrder create
app.MapPost("/tuberorders", (TuberOrderDTO tuberOrderDTO) =>
{
    TuberOrder newTuberOrder = new TuberOrder
    {
        Id = tuberOrders.Max(toppings => toppings.Id) + 1,
        OrderPlacedOnDate = DateOnly.FromDateTime(DateTime.Now),
        CustomerId = tuberOrderDTO.CustomerId,
        TuberDriverId = tuberOrderDTO.TuberDriverId,
        DeliveredOnDate = tuberOrderDTO.DeliveredOnDate
    };

    tuberOrders.Add(newTuberOrder);

    return Results.Created(
        $"/tuberorders/{newTuberOrder.Id}",
        new TuberOrderDTO
        {
            Id = newTuberOrder.Id,
            OrderPlacedOnDate = newTuberOrder.OrderPlacedOnDate,
            CustomerId = newTuberOrder.CustomerId,
            TuberDriverId = newTuberOrder.TuberDriverId,
            DeliveredOnDate = newTuberOrder.DeliveredOnDate
        }
    );
});

//TuberOrders assign driver
app.MapPut("/tuberorders/{id}", (int id, TuberOrderDTO tuberOrderDTO) =>
{
    TuberOrder orderToUpdate = tuberOrders.FirstOrDefault(t => t.Id == id);
    if (orderToUpdate == null)
    {
        return Results.NotFound();
    }

    TuberDriver driver = tuberDrivers.FirstOrDefault(d => d.Id == tuberOrderDTO.TuberDriverId);
    if (driver == null)
    {
        return Results.BadRequest("No driver exists with that Id.");
    }

    orderToUpdate.TuberDriverId = tuberOrderDTO.TuberDriverId;

    return Results.NoContent();
});

//TuberOrders complete
app.MapPost("/tuberorders/{id}/complete", (int id) =>
{
    TuberOrder orderToComplete = tuberOrders.FirstOrDefault(t => t.Id == id);
    if (orderToComplete == null)
    {
        return Results.NotFound();
    }

    orderToComplete.DeliveredOnDate = DateOnly.FromDateTime(DateTime.Now);

    return Results.NoContent();
});

//Toppings get all
app.MapGet("/toppings", () =>
{
    return toppings.Select(t => new ToppingDTO
    {
        Id = t.Id,
        Name = t.Name
    });
});

//Toppings get by id
app.MapGet("/toppings/{id}", (int id) =>
{
    Topping topping = toppings.FirstOrDefault(t => t.Id == id);
    if (topping == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new ToppingDTO
    {
        Id = topping.Id,
        Name = topping.Name
    });
});



app.Run();
//don't touch or move this!
public partial class Program { }