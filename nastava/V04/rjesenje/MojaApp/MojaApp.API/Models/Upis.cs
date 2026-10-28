namespace MojaApp.API.Models;

// Veza student–predmet (M:N) sa svojim podacima: kada je student upisao predmet i koju je ocjenu dobio.
public class Upis
{
    public int Id { get; set; }

    public int StudentId { get; set; }          // strani ključ (FK)
    public Student Student { get; set; } = null!; // navigacija

    public int PredmetId { get; set; }
    public Predmet Predmet { get; set; } = null!;

    public DateTime DatumUpisa { get; set; }
    public int? Ocjena { get; set; }             // null dok predmet nije položen
}
