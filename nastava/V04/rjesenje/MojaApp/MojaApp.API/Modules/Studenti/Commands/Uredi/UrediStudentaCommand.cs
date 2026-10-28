using System.Text.Json.Serialization;

namespace MojaApp.API.Modules.Studenti.Commands.Uredi;

// Zadatak 5 (dodatno). Broj indeksa se ne mijenja, pa ga ovdje nema.
public class UrediStudentaCommand : IRequest
{
    [JsonIgnore] // Id dolazi iz rute (PUT /Studenti/5), ne iz tijela zahtjeva
    public int Id { get; set; }

    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateOnly? DatumRodjenja { get; set; }
    public int? SmjerId { get; set; }
}
