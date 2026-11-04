using MojaApp.Domain.Common;

namespace MojaApp.Domain.Entities;

// Jedan smjer ima više studenata (1:N).
public class Smjer : BaseEntity
{
    public string Naziv { get; set; } = "";

    // Navigacija na "više" stranu veze: svi studenti ovog smjera.
    public List<Student> Studenti { get; set; } = [];
}
