namespace MojaApp.API.Modules.Studenti.Queries.Lista;

// Query: samo čita. Rezultat je lista studenata.
public class ListaStudenataQuery : IRequest<List<StudentResponse>>
{
    public string? Pretraga { get; set; } // ime, prezime ili broj indeksa
}
