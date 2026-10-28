using System.Text.Json.Serialization;

namespace MojaApp.API.Modules.Upisi.Commands.Ocjena;

// Zadatak 3: zamjena za OcjenaRequest.
public class UpisiOcjenuCommand : IRequest
{
    [JsonIgnore] // Id upisa dolazi iz rute
    public int UpisId { get; set; }

    public int Ocjena { get; set; }
}
