using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/code-scanning", async (string userName, string password, string value) =>
{
    var connString = $"Host=myserver;Username={userName};Password={password};Database=mydatabase";

    await using var conn = new NpgsqlConnection(connString);
    await conn.OpenAsync();

    await using var cmd = new NpgsqlCommand($"SELECT some_field FROM data where some_field = {value}", conn);
    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
        Console.WriteLine(reader.GetString(0));
});

app.Run();
