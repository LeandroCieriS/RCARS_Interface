using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RC_QueryService.Conection;
using RCARS.Interface.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RCDatabase>(options =>
{
    options.UseSqlServer("Data Source = NZXT; Initial Catalog = RCARSDB; User ID = sa; Password = nzxtadmin");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RCDatabase>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Vehicles", async (RCDatabase db) =>
{
    return await db.Vehicles.ToListAsync();
})
.WithName("GetAllVehicles")
.Produces<List<Vehicle>>(StatusCodes.Status200OK)
.WithTags("VehiclesController");

app.MapPost("/Vehicles", async ([FromBody] Vehicle addVehicle, [FromServices] RCDatabase db, HttpResponse response) =>
{
    db.Vehicles.Add(addVehicle);
    await db.SaveChangesAsync();
    response.StatusCode = 200;
    response.Headers.Location = $"vehicles/{addVehicle.NumberPlate}";
})
.Accepts<Vehicle>("application/json")
.Produces<Vehicle>(StatusCodes.Status201Created)
.WithName("AddNewVehicle").WithTags("VehiclesController");

app.MapGet("/Vehicles/{numberPlate}", async (RCDatabase db, string numberPlate) =>
    await db.Vehicles.SingleOrDefaultAsync(s => s.NumberPlate == numberPlate) is Vehicle myVehicle ? Results.Ok(myVehicle) : Results.NotFound()
)
.Produces<Vehicle>(StatusCodes.Status200OK)
.WithName("GetVehicleByNumberPlate").WithTags("VehiclesController");

app.MapGet("/Customers/{Id}", async (RCDatabase db, string id) =>
    await db.Customers.SingleOrDefaultAsync(s => s.Id.ToString() == id) is Customer loggedCustomer ? Results.Ok(loggedCustomer) : Results.NotFound()
)
.Produces<Vehicle>(StatusCodes.Status200OK)
.WithName("GetLoggedCustomer").WithTags("CustomersController");

app.MapGet("/Purchases/{Id}", async (RCDatabase db, string id) =>
    await db.Purchases.Where(s => s.Id.ToString() == id).ToListAsync() is List<Purchase> purchases ? Results.Ok(purchases) : Results.NotFound()
)
.Produces<Vehicle>(StatusCodes.Status200OK)
.WithName("GetPurchasesById").WithTags("PurchasesController");

app.MapGet("/Sales/{Id}", async (RCDatabase db, int id) =>
    await db.Sales.Where(s => s.Id == id).ToListAsync() is List<Sale> sales ? Results.Ok(sales) : Results.NotFound()
)
.Produces<Vehicle>(StatusCodes.Status200OK)
.WithName("GetSalesById").WithTags("SalesController");

app.MapGet("/Customers/{customerId}/vehicles", async (RCDatabase db, string customerId) =>
{
    var sales = await db.Sales.Where(s => s.CustomerId.ToString() == customerId).ToListAsync();
    var vehicles = new List<Vehicle>();
    foreach (var s in sales)
    {
        var vehicle = await db.Vehicles.SingleOrDefaultAsync(v => v.NumberPlate == s.NumberPlate);
        vehicles.Add(vehicle);
    }
    return vehicles.Count > 0 ? Results.Ok(vehicles) : Results.NotFound();
})
.Produces<List<Vehicle>>(StatusCodes.Status200OK)
.WithName("GetVehiclesOfCustomer").WithTags("VehiclesController");

//app.MapGet("/customers", async (RCDatabase db) =>
//{
//    return await db.Customers.ToListAsync();
//})
//.WithName("GetAllCustomers")
//.Produces<List<Customer>>(StatusCodes.Status200OK)
//.WithTags("CustomersController");

//app.MapGet("/purchases", async (RCDatabase db) =>
//{
//    return await db.Purchases.ToListAsync();
//})
//.WithName("GetAllPurchases")
//.Produces<List<Purchase>>(StatusCodes.Status200OK)
//.WithTags("PurchasesController");

//app.MapGet("/sales", async (RCDatabase db) =>
//{
//    return await db.Sales.ToListAsync();
//})
//.WithName("GetAllSales")
//.Produces<List<Sale>>(StatusCodes.Status200OK)
//.WithTags("SalesController");

app.MapGet("/Customer/{userEmail}", async (RCDatabase db, string userEmail) =>
{
    var user = await db.Users.Where(u => u.Email.ToString() == userEmail).FirstOrDefaultAsync();
    Customer? customer = null;
    if (user != null)
        customer = await db.Customers.SingleOrDefaultAsync(c => c.Id == user.UserGUID);
    
    return customer == null ? Results.NotFound() : Results.Ok(customer);
})
.Produces<Customer>(StatusCodes.Status200OK)
.WithName("GetCustomerOfUser").WithTags("CustomerController");

app.Run();

public partial class Program { }