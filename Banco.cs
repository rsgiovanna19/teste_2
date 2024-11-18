using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Livro> Livros => Set<Livro>();
}

//criando objetos 
public class Banco{
    private static List<Livro> livros = new List<Livro>
    {
        new Livro { Id = 1, Autor = "Dan Brown", Nome = "O símbolo perdido", LeituraCompleta = false },
        new Livro { Id = 2, Autor = "Dan Brown", Nome = "Diário de um banana", LeituraCompleta = true },
    };

}

//nao eh necessário após o appDbContext
/*    public static List<Livro> getLivros(){
        return livros;
    }

    public static Livro getLivro(int id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return livros.FirstOrDefault(t => t.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
    }

      public static Livro addLivro(Livro livro)
    {
        livro.Id = livros.Count + 1;
        livros.Add(livro);
        return livro;
    }

    public static Livro? updateLivro(int id, Livro livro)
    {
        var livroExistente = livros.FirstOrDefault(t => t.Id == id);
        if (livroExistente == null)
        {
            return null;
        }

        livroExistente.Nome = livro.Nome;
        livroExistente.Autor = livro.Autor;
        livroExistente.LeituraCompleta = livro.LeituraCompleta;
        return livroExistente;
    }
     public static bool deleteLivro(int id)
    {
        var livroExistente = livros.FirstOrDefault(t => t.Id == id);
        if (livroExistente == null)
        {
            return false;
        }

        livros.Remove(livroExistente);
        return true;
    }*/



