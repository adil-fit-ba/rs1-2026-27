namespace MojaApp.API.Models;

public class Student
{
    public int Id { get; set; }
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public string BrojIndeksa { get; set; } = "";
    public DateOnly? DatumRodjenja { get; set; }
    public DateTime DatumKreiranja { get; set; }

    // 1:N – student pripada jednom smjeru. Nullable, jer studenti iz V02 već postoje u bazi bez smjera.
    public int? SmjerId { get; set; }
    public Smjer? Smjer { get; set; }

    // M:N – predmeti studenta, preko upisa.
    public List<Upis> Upisi { get; set; } = [];
}
