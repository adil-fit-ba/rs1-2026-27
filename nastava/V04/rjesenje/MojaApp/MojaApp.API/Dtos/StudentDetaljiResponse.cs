namespace MojaApp.API.Dtos;

// GET /Studenti/{id}: student sa svojim upisima.
public class StudentDetaljiResponse
{
    public int Id { get; set; }
    public string ImePrezime { get; set; } = "";
    public string BrojIndeksa { get; set; } = "";
    public string? Smjer { get; set; }
    public List<StudentUpisResponse> Upisi { get; set; } = [];
}

public class StudentUpisResponse
{
    public int UpisId { get; set; }
    public string Predmet { get; set; } = "";
    public string Sifra { get; set; } = "";
    public DateTime DatumUpisa { get; set; }
    public int? Ocjena { get; set; }
}
