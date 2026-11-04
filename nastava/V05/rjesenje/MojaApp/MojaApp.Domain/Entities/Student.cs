using MojaApp.Domain.Common;

namespace MojaApp.Domain.Entities;

// Id, IsDeleted, CreatedAtUtc i ModifiedAtUtc su naslijeđeni iz BaseEntity
// (CreatedAtUtc zamjenjuje DatumKreiranja iz V02–V04).
public class Student : BaseEntity
{
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public string BrojIndeksa { get; set; } = "";
    public DateOnly? DatumRodjenja { get; set; }

    // 1:N – student pripada jednom smjeru (nije obavezno).
    public int? SmjerId { get; set; }
    public Smjer? Smjer { get; set; }

    // M:N – predmeti studenta, preko upisa.
    public List<Upis> Upisi { get; set; } = [];
}
