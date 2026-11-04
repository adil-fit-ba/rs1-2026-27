using MojaApp.Domain.Common;

namespace MojaApp.Domain.Entities;

public class Predmet : BaseEntity
{
    public string Naziv { get; set; } = "";
    public string Sifra { get; set; } = "";
    public int Ects { get; set; }

    // Studenti nisu direktno ovdje: veza ide preko upisa (M:N s podacima).
    public List<Upis> Upisi { get; set; } = [];
}
