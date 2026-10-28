using System.ComponentModel.DataAnnotations;

namespace MojaApp.API.Dtos;

public class UpisDodajRequest
{
    [Range(1, int.MaxValue)]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue)]
    public int PredmetId { get; set; }
}
