namespace MojaApp.API.Dtos;

public class UpisResponse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Student { get; set; } = "";
    public int PredmetId { get; set; }
    public string Predmet { get; set; } = "";
    public DateTime DatumUpisa { get; set; }
    public int? Ocjena { get; set; }
}
