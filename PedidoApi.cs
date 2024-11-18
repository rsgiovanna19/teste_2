using Microsoft.EntityFrameworkCore;

public static class PedidoApi
{
    public static void MapPedidoApi(this WebApplication app)
    {
        // Endpoint GET para listar todos os pedidos de forma assíncrona
        app.MapGet("/pedidos", async (Banco db) =>
            await db.Pedidos.ToListAsync());

        // Endpoint GET para buscar um pedido por ID
        app.MapGet("/pedidos/{id}", async (int id, Banco db) =>
            await db.Pedidos.FindAsync(id) is Pedido pedido
                ? Results.Ok(pedido)
                : Results.NotFound());

        // Endpoint POST para criar um novo pedido (recebendo o objeto do corpo da requisição)
        app.MapPost("/pedidos", async (Pedido pedido, Banco db) =>
        {
            db.Pedidos.Add(pedido);
            await db.SaveChangesAsync();
            return Results.Created($"/pedidos/{pedido.Id}", pedido);
        });

        // Endpoint PUT para atualizar um pedido
        app.MapPut("/pedidos/{id}", async (int id, Pedido pedidoAlterado, Banco db) =>
        {
            var pedido = await db.Pedidos.FindAsync(id);
            if (pedido is null) return Results.NotFound();

            // Atualiza o status do pedido
            pedido.Realizado = pedidoAlterado.Realizado;

            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        // Endpoint DELETE para deletar um pedido
        app.MapDelete("/pedidos/{id}", async (int id, Banco db) =>
        {
            if (await db.Pedidos.FindAsync(id) is Pedido pedido)
            {
                db.Pedidos.Remove(pedido);
                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}
