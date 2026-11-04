namespace MojaApp.Application.Modules.Studenti.Queries.GetById;

public class StudentGetByIdQuery : IRequest<StudentDetaljiResponse>
{
    public int Id { get; set; }
}
