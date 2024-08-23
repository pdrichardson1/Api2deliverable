using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("UserList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/users", async (UserDb db) =>
    await db.Users.ToListAsync());

app.MapGet("/users/complete", async (UserDb db) =>
    await db.Users.Where(u => u.IsComplete).ToListAsync());

app.MapGet("/users/{id}", async (int id, UserDb db) =>
    await db.Users.FindAsync(id)
        is User user
            ? Results.Ok(user)
            : Results.NotFound());

app.MapPost("/users", async (User user, UserDb db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", async (int id, User inputUser, UserDb db) =>
{
    var user = await db.Users.FindAsync(id);

    if (user is null) return Results.NotFound();

    user.FirstName = inputUser.FirstName;
    user.LastName = inputUser.LastName;
    user.IsComplete = inputUser.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/user/{id}", async (int id, UserDb db) =>
{
    if (await db.Users.FindAsync(id) is User user)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapGet("/superadmins", async (UserDb db) =>
    await db.SuperAdmins.ToListAsync());

app.MapGet("/superadmins/{id}", async (int id, UserDb db) =>
    await db.SuperAdmins.FindAsync(id)
        is SuperAdmin superAdmin
            ? Results.Ok(superAdmin)
            : Results.NotFound());

app.MapPost("/superadmins", async (SuperAdmin superAdmin, UserDb db) =>
{
    db.SuperAdmins.Add(superAdmin);
    await db.SaveChangesAsync();

    return Results.Created($"/superadmins/{superAdmin.Id}", superAdmin);
});

app.MapPut("/superadmins/{id}", async (int id, SuperAdmin inputSuperAdmin, UserDb db) =>
{  
    var superAdmin = await db.SuperAdmins.FindAsync(id);

    if (superAdmin is null) return Results.NotFound();

    superAdmin.FirstName = inputSuperAdmin.FirstName;
    superAdmin.LastName = inputSuperAdmin.LastName;
    superAdmin.CanSeeAll = inputSuperAdmin.CanSeeAll;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/superadmins/{id}", async (int id, UserDb db) =>
{
    if (await db.SuperAdmins.FindAsync(id) is SuperAdmin superAdmin)
    {
        db.SuperAdmins.Remove(superAdmin);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.MapGet("/supervisors", async (UserDb db) =>
    await db.Supervisors.ToListAsync());

app.MapGet("/supervisors/{id}", async (int id, UserDb db) =>
    await db.Supervisors.FindAsync(id)
        is Supervisor supervisor
            ? Results.Ok(supervisor)
            : Results.NotFound());

app.MapPost("/supervisors", async (Supervisor supervisor, UserDb db) =>
{
    db.Supervisors.Add(supervisor);
    await db.SaveChangesAsync();

    return Results.Created($"/supervisors/{supervisor.Id}", supervisor);
});

app.MapPut("/supervisors/{id}", async (int id, Supervisor inputSupervisor, UserDb db) =>
{
    var supervisor = await db.Supervisors.FindAsync(id);

    if (supervisor is null) return Results.NotFound();

    supervisor.FirstName = inputSupervisor.FirstName;
    supervisor.LastName = inputSupervisor.LastName;
    supervisor.IsManager = inputSupervisor.IsManager;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/supervisors/{id}", async (int id, UserDb db) =>
{
    if (await db.Supervisors.FindAsync(id) is Supervisor supervisor)
    {
        db.Supervisors.Remove(supervisor);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();