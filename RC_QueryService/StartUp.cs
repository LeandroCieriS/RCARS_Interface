using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RC_QueryService.Conection;
using RCARS.Interface.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RCDatabase>(options => {
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

app.MapGet("/customers", async (RCDatabase db) =>
{
    return await db.Customers.ToListAsync();
})
.WithName("GetAllCustomers")
.Produces<List<Customer>>(StatusCodes.Status200OK)
.WithTags("CustomersController");

app.MapGet("/purchases", async (RCDatabase db) =>
{
    return await db.Purchases.ToListAsync();
})
.WithName("GetAllPurchases")
.Produces<List<Purchase>>(StatusCodes.Status200OK)
.WithTags("PurchasesController");

app.MapGet("/sales", async (RCDatabase db) =>
{
    return await db.Sales.ToListAsync();
})
.WithName("GetAllSales")
.Produces<List<Sale>>(StatusCodes.Status200OK)
.WithTags("SalesController");

app.Run();
