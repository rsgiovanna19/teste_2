using Microsoft.EntityFrameworkCore;


public static class ClienteApi{

public static void MapClienteApi(this WebApplication app)
{

// Endpoint GET para listar todos os clientes de forma assíncrona
app.MapGet("/cliente", async (Banco db) =>
    await db.Clientes.ToListAsync());

// Endpoint GET para buscar por ID
app.MapGet("/cliente/{id}", async (int id, Banco db) =>
    await db.Clientes.FindAsync(id) is Cliente cliente
        ? Results.Ok(cliente)
        : Results.NotFound());

// Endpoint POST para criar um novo  (recebendo o objeto do corpo da requisição)
app.MapPost("/cliente", async (Cliente cliente, Banco db) => {
    db.Clientes.Add(cliente);
    await db.SaveChangesAsync();
    return Results.Created($"/cliente/{cliente.Id}", cliente);
});

// Endpoint PUT para atualizar 
app.MapPut("/cliente/{id}", async (int id, Cliente clienteAlterado, Banco db) =>
{
    var cliente = await db.Clientes.FindAsync(id);
    if (cliente is null) return Results.NotFound();

    cliente.Nome = clienteAlterado.Nome;
    cliente.Comprado = clienteAlterado.Comprado;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint DELETE para deletar um cliente
app.MapDelete("/cliente/{id}", async (int id, Banco db) =>
{
    if (await db.Clientes.FindAsync(id) is Cliente cliente)
    {
        db.Clientes.Remove(cliente);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});
}
}