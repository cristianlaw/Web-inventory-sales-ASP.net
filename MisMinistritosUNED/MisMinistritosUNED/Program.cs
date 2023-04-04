using MisMinistritosUNED.Connection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ICliente, ClienteConnection>();
builder.Services.AddScoped<IProveedor, ProveedorConnection>();
builder.Services.AddScoped<IProducto, ProductoConnection>();
builder.Services.AddScoped<ITour, TourConnection>();
builder.Services.AddScoped<IFactura, FacturaConnection>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
