namespace MojaApp.API.Dtos;

public class PredmetResponse
{
    public int Id { get; set; }
    public string Naziv { get; set; } = "";
    public string Sifra { get; set; } = "";
    public int Ects { get; set; }
    public int BrojUpisanih { get; set; }
}
