namespace MojaApp.API.Models;

// Jedan smjer ima više studenata (1:N).
public class Smjer
{
    public int Id { get; set; }
    public string Naziv { get; set; } = "";

    // Navigacija na "više" stranu veze: svi studenti ovog smjera.
    public List<Student> Studenti { get; set; } = [];
}
