namespace MojaApp.API.Dtos;

public class PredmetStatistikaResponse
{
    public int PredmetId { get; set; }
    public string Naziv { get; set; } = "";
    public int BrojUpisanih { get; set; }
    public int BrojPolozenih { get; set; }
    public double? ProsjecnaOcjena { get; set; } // null dok niko nije položio
}
