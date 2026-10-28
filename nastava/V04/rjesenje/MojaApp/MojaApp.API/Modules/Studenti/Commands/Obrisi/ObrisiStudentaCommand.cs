namespace MojaApp.API.Modules.Studenti.Commands.Obrisi;

// IRequest bez tipa: command ne vraća podatke (kontroler vraća 204).
public class ObrisiStudentaCommand : IRequest
{
    public int Id { get; set; }
}
