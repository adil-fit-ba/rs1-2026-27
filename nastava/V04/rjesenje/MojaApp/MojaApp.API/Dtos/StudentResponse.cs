namespace MojaApp.API.Dtos;

// Ono što server vraća. Klijent ne dobija entitet direktno.
public class StudentResponse
{
    public int Id { get; set; }
    public string ImePrezime { get; set; } = "";
    public string BrojIndeksa { get; set; } = "";
    public DateOnly? DatumRodjenja { get; set; }
    public string? Smjer { get; set; } // naziv smjera, ne cijeli entitet
}
