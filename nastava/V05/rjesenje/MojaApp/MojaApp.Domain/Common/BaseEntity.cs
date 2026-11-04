namespace MojaApp.Domain.Common;

// Zajednička polja svih entiteta (kao Market.Domain/Common/BaseEntity.cs u starteru).
// Vrijednosti CreatedAtUtc, ModifiedAtUtc i IsDeleted postavlja AppDbContext pri SaveChanges, ne handler.
public class BaseEntity
{
    public int Id { get; set; }

    // Soft delete: zapis se ne briše iz tabele, nego dobija IsDeleted = true.
    public bool IsDeleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModifiedAtUtc { get; set; } // null dok zapis nije mijenjan
}
