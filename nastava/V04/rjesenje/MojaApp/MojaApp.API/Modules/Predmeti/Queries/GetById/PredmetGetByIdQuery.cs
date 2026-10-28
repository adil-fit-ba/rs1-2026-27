namespace MojaApp.API.Modules.Predmeti.Queries.GetById;

// Zadatak 1
public class PredmetGetByIdQuery : IRequest<PredmetResponse>
{
    public int Id { get; set; }
}
