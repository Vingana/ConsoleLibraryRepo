using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGenWithAuth();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }

    await scope.SeedRolesAsync();
    await scope.SeedAdminUserAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapControllers();

app.Run();