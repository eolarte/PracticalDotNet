using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShoppingListDb>(opt => opt.UseInMemoryDatabase("ShoppingList"));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World! This is quick Api to store your shopping list");
app.MapGet("/shopping/list", async (ShoppingListDb db) => await db.Items.ToArrayAsync()).Produces<List<Item>>();

app.MapGet("/shopping/{id}", async (int id, ShoppingListDb db) =>
    await db.Items.FirstOrDefaultAsync(x => x.Id == id) is { } item
        ? Results.Ok(item)
        : Results.NotFound())
    .Produces<Item>()
    .Produces(StatusCodes.Status404NotFound);

app.MapPost("/shopping/add", async (Item item, ShoppingListDb db) =>
{
    await db.Items.AddAsync(item);
    await db.SaveChangesAsync();
    return Results.Created($"/shopping/{item.Id}", item);
});

app.MapPut("/shopping/{id}", async (int id, Item editedItem, ShoppingListDb db) =>
{
    if (await db.Items.FindAsync(id) is not { } todo) return Results.NotFound();

    todo.Name = editedItem.Name;
    todo.Priority = editedItem.Priority;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/shopping/delete/{id}", async (int id, ShoppingListDb db) =>
{
    if (await db.Items.FindAsync(id) is not { } todo) return Results.NotFound();

    db.Items.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok(todo);
}).Produces<Item>().Produces(StatusCodes.Status404NotFound);

app.Run();

public class ShoppingListDb : DbContext
{
    public ShoppingListDb(DbContextOptions<ShoppingListDb> options)
        : base(options)
    {
    }
    public DbSet<Item> Items => Set<Item>();
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
}