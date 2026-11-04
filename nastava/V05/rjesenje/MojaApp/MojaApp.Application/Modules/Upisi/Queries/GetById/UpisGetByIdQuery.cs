namespace MojaApp.Application.Modules.Upisi.Queries.GetById;

public class UpisGetByIdQuery : IRequest<UpisResponse>
{
    public int Id { get; set; }
}
