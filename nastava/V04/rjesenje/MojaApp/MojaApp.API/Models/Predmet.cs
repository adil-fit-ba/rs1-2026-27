namespace MojaApp.API.Models;

public class Predmet
{
    public int Id { get; set; }
    public string Naziv { get; set; } = "";
    public string Sifra { get; set; } = "";
    public int Ects { get; set; }

    // Studenti nisu direktno ovdje: veza ide preko upisa (M:N s podacima).
    public List<Upis> Upisi { get; set; } = [];
}
