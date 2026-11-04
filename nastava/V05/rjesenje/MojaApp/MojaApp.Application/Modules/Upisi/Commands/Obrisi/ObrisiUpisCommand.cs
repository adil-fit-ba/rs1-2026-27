namespace MojaApp.Application.Modules.Upisi.Commands.Obrisi;

// DELETE /Upisi/5: ispis s predmeta, samo dok ocjena nije upisana.
public class ObrisiUpisCommand : IRequest
{
    public int Id { get; set; }
}
