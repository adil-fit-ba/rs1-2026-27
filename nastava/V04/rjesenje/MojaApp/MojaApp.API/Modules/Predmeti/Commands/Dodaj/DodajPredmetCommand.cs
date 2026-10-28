namespace MojaApp.API.Modules.Predmeti.Commands.Dodaj;

// Zadatak 2: zamjena za PredmetDodajRequest. Vraća Id novog predmeta.
public class DodajPredmetCommand : IRequest<int>
{
    public string Naziv { get; set; } = "";
    public string Sifra { get; set; } = "";
    public int Ects { get; set; }
}
