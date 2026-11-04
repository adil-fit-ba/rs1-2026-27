namespace MojaApp.Application.Modules.Upisi.Commands.Dodaj;

// POST /Upisi: upis studenta na predmet (zamjena za UpisDodajRequest). Vraća Id novog upisa.
// DatumUpisa postavlja server, pa ga ovdje nema.
public class DodajUpisCommand : IRequest<int>
{
    public int StudentId { get; set; }
    public int PredmetId { get; set; }
}
