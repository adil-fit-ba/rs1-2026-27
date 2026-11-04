namespace MojaApp.Application.Modules.Smjerovi.Queries.Lista;

// GET /Smjerovi (u V04 je bio direktno u kontroleru, nad AppDbContext)
public class ListaSmjerovaQuery : IRequest<List<SmjerResponse>>;
