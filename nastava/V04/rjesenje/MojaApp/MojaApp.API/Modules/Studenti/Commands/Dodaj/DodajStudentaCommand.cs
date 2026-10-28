namespace MojaApp.API.Modules.Studenti.Commands.Dodaj;

// Command: mijenja podatke. Klijent ga šalje kao tijelo POST zahtjeva (umjesto StudentDodajRequest iz V03).
// Id i DatumKreiranja postavlja server, pa ih ovdje nema.
public class DodajStudentaCommand : IRequest<StudentResponse>
{
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public string BrojIndeksa { get; set; } = "";
    public DateOnly? DatumRodjenja { get; set; }
    public int? SmjerId { get; set; } // smjer nije obavezan
}
