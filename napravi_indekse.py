"""Pravi početnu stranicu (index.html) i stranice samostalnih vježbi za GitHub Pages repozitorija rs1-2026-27, grana gh-pages.
Izgled kao MiS 2026/27. Dugme „Kopiraj“ za kod: kopiraj-kod.js (ista skripta kao u prezentacijama V04)."""
import html, io, os, sys
REPO = sys.argv[1] if len(sys.argv) > 1 else os.path.dirname(os.path.abspath(__file__))
CSS = """<style>
:root { --papir: #eef1f4; --list: #fff; --tinta: #17212b; --siva: #56636f; --linija: #d3dae1; --narandza: #b9531a; --narandza-meko: #fbeee5;
  --za: #345da9; --kod-bg: #14213d; --kod-tx: #f3f5f8; --kod-kom: #f2c14e;
  --f-naslov: Cambria, Georgia, serif; --f-tekst: "Segoe UI", "Helvetica Neue", Arial, sans-serif; --f-mono: Consolas, "Courier New", monospace; }
@media (prefers-color-scheme: dark) { :root { --papir: #10161c; --list: #18212a; --tinta: #e3e9ee; --siva: #9ba8b4; --linija: #2a3540; --narandza: #f0894a; --narandza-meko: #2c2119; --za: #7aa2e3; --kod-bg: #0b1320; color-scheme: dark; } }
* { box-sizing: border-box; }
body { margin: 0; background: var(--papir); color: var(--tinta); font: 17px/1.5 var(--f-tekst); }
main { max-width: 960px; margin: 0 auto; padding: 2rem max(16px, env(safe-area-inset-left)) 3rem; }
h1, h2, h3 { font-family: var(--f-naslov); line-height: 1.2; margin: 0 0 0.4rem; }
h1 { font-size: 2.1rem; } h2 { font-size: 1.6rem; margin-top: 2.6rem; padding-top: 1.2rem; border-top: 1px solid var(--linija); }
h3 { font-size: 1.2rem; margin-top: 1.6rem; }
.oznaka { font-size: 0.8rem; letter-spacing: 0.08em; text-transform: uppercase; color: var(--narandza); }
.siva { color: var(--siva); }
.veze { display: grid; gap: 0.7rem; grid-template-columns: repeat(auto-fit, minmax(min(100%, 17rem), 1fr)); margin-top: 0.8rem; }
.veze a { display: block; text-decoration: none; background: var(--list); border: 1px solid var(--linija); border-radius: 6px; padding: 0.8rem 1rem; color: var(--tinta); }
.veze a:hover, .veze a:focus-visible { border-color: var(--narandza); }
.veze b { color: var(--narandza); }
.veze span { display: block; color: var(--siva); font-size: 0.9rem; }
.veze a.istaknuto { background: var(--narandza-meko); border: 2px solid var(--narandza); }
.veze a.istaknuto b::after { content: " →"; }
a { color: var(--narandza); }
code { font-family: var(--f-mono); font-size: 0.88em; background: var(--list); border: 1px solid var(--linija); border-radius: 3px; padding: 0 0.25em; overflow-wrap: anywhere; }
.tab-okvir { overflow-x: auto; margin: 0.6rem 0; }
table { border-collapse: collapse; width: 100%; font-size: 0.92rem; background: var(--list); }
th, td { border: 1px solid var(--linija); padding: 0.4rem 0.55rem; text-align: left; vertical-align: top; }
th { background: var(--kod-bg); color: #fff; font-weight: 600; }
ul { padding-left: 1.3rem; } li { margin: 0.2rem 0; }
.cilj { background: var(--list); border-left: 4px solid var(--narandza); padding: 0.7rem 1rem; border-radius: 4px; }
.kod { position: relative; background: var(--kod-bg); color: var(--kod-tx); border-radius: 6px; margin: 0.6rem 0; }
.kod .jezik { font: 600 0.78rem var(--f-tekst); color: #c9d3e0; padding: 0.5rem 6.5rem 0.4rem 0.9rem; border-bottom: 1px solid rgb(255 255 255 / 0.12); }
.kod pre { margin: 0; padding: 0.7rem 0.9rem; overflow-x: auto; font: 0.86rem/1.5 var(--f-mono); }
.kod .kom { color: var(--kod-kom); }
.kod code { background: none; border: 0; padding: 0; font-size: inherit; color: inherit; }
.kopiraj { position: absolute; top: 0.32rem; right: 0.45rem; z-index: 2; font: 600 0.74rem/1.2 var(--f-tekst); padding: 0.2rem 0.6rem;
  border: 1px solid rgb(255 255 255 / 0.4); border-radius: 4px; background: var(--kod-bg); color: #fff; cursor: pointer; }
.kopiraj:hover, .kopiraj:focus-visible { border-color: var(--narandza); }
.kopiraj.ok { border-color: #7fd4a0; }
@media print { .kopiraj { display: none; } }
footer { margin-top: 2.5rem; font-size: 0.85rem; color: var(--siva); border-top: 1px solid var(--linija); padding-top: 0.8rem; }
</style>"""
k = lambda href, naslov, opis, kl="": f'    <a href="{href}"{f" class={chr(34)}{kl}{chr(34)}" if kl else ""}><b>{naslov}</b><span>{opis}</span></a>\n'
grupa = lambda naslov, kartice: f'  <h3>{naslov}</h3>\n  <div class="veze">\n{"".join(kartice)}  </div>\n'
GH = "https://github.com/adil-fit-ba/rs1-2026-27/tree/main/nastava/V04/rjesenje/MojaApp"


def v04(p):
    return (grupa("Samostalna vježba", [
        k(p + "samostalna-vjezba-nastavnik.html", "Samostalna vježba: entitet Nastavnik",
          "Nova entity klasa s GetById i Dodaj, po obrascu iz V04: model, migracija, query, command, validator, kontroler i provjera.", "istaknuto")])
      + grupa("Prezentacije", [
        k(p + "v04-nadogradnja-v03-na-cqrs.html", "Nadogradnja V03 na CQRS", "Klasični kontroler naspram handlera, pa sedam koraka, komponenta po komponenta: paketi, izuzeci, GlobalExceptionHandler, ValidationBehavior, queries, commands, čišćenje (32 slajda)."),
        k(p + "v04-nadogradnja-v03-na-cqrs.pdf", "Nadogradnja V03 na CQRS (PDF)", "Ista prezentacija za štampu ili čitanje bez interneta."),
        k(p + "v04-put-zahtjeva-kroz-slojeve.html", "Put zahtjeva kroz slojeve", "Živi primjer: zahtjev prolazi kroz kontroler, validator i handler korak po korak, sa SQL-om i odgovorom.")])
      + grupa("Kod", [
        k(p + "V04-MojaApp-pocetni-kod-dotnet8.zip", "MojaApp: početni kod (ZIP)", "Rješenje vježbi 3, .NET 8. Od njega počinje nadogradnja."),
        k(GH, "MojaApp: rješenje (GitHub)", "Kod nakon vježbi 4, u repozitoriju: folder nastava/V04/rjesenje.")]))

GH5 = "https://github.com/adil-fit-ba/rs1-2026-27/tree/main/nastava/V05/rjesenje/MojaApp"


GH5CA = "https://github.com/adil-fit-ba/rs1-2026-27/tree/main/nastava/V05/rjesenje-clean-architecture/MojaApp"


def v05(p):
    return ('  <p class="siva">Redoslijed rada: 1 → 2 → 3 na času, 4 kao nastavak. Svaki korak počinje od projekta iz prethodnog koraka.</p>\n'
      + grupa("1. Početni kod", [
        k(GH, "MojaApp: rješenje V04 (GitHub)", "Polazna tačka časa: vlastiti projekat nakon V04 ili folder nastava/V04/rjesenje.")])
      + grupa("2. Postojeći projekat na Clean Architecture", [
        k(p + "v05-postojeci-projekat-na-clean-architecture.html", "Postojeći projekat na Clean Architecture, korak po korak",
          "Projekat nakon V04 u pet koraka postaje Domain, Application, Infrastructure i API, uz istu bazu podataka. Solution Explorer po koraku: klik na fajl pokazuje kod s označenim izmjenama.", "istaknuto"),
        k(GH5CA, "Rješenje koraka 2 (GitHub)", "V04 u četiri projekta, svi kontroleri kroz MediatR, ista baza podataka: folder nastava/V05/rjesenje-clean-architecture."),
        k(p + "v05-slojevi-clean-architecture.html", "Slojevi Clean Architecture", "Interaktivno: klik na sloj pokazuje projekat, reference i kod; put zahtjeva POST /Studenti u osam koraka; kviz „u koji projekat ide“.")])
      + grupa("3. Zadaci: novi CRUD na gotov projekat", [
        k(p + "v05-zadaci-clean-architecture.html", "Zadaci: Sala, Ispit i raspored sale",
          "Polazna tačka je projekat iz koraka 2. Novi entiteti s CRUD-om: validator, handler (404, 409), konfiguracija, migracija i .http sa očekivanim statusima.", "istaknuto")])
      + grupa("4. Nastavak: BaseEntity i soft delete", [
        k(p + "v05-clean-architecture-i-soft-delete.html", "Clean Architecture i soft delete", "Osam koraka od V04: koraci 1–5 su podjela na četiri projekta (ovdje bez starih migracija), zatim BaseEntity, soft delete, global query filter i nova baza podataka MojaApp_V05 (29 slajdova)."),
        k(p + "v05-clean-architecture-i-soft-delete.pdf", "Clean Architecture i soft delete (PDF)", "Ista prezentacija za štampu ili čitanje bez interneta."),
        k(p + "samostalne-vjezbe.html", "Samostalne vježbe: Nastavnik, obrisani predmeti, vraćanje, brisanje smjera",
          "Polazna tačka je rješenje V05. Četiri zadatka: novi entitet s BaseEntity, IgnoreQueryFilters, vraćanje obrisanog zapisa i zaštita povezanih podataka."),
        k(GH5, "MojaApp: rješenje V05 (GitHub)", "Kod nakon vježbi 5, folder nastava/V05/rjesenje: četiri projekta, BaseEntity, soft delete, nova baza podataka MojaApp_V05.")]))


GH6 = "https://github.com/adil-fit-ba/rs1-2026-27/tree/main/nastava/V06/pocetni/Market.Backend"


def v06(p):
    m = p + "v06-market-backend-starter.html"
    return ('  <p class="siva">Novi početni kod: backend početnog projekta za projektni zadatak (Market), pripremljen na .NET 8 i LocalDB. Na narednom času isti projekat se koristi za narudžbu sa stavkama.</p>\n'
      + grupa("1. Početni kod", [
        k(p + "V06-Market-Backend-starter-dotnet8_v2.zip", "Market Backend starter (ZIP)", "Šest projekata na .NET 8, baza podataka Market_V06 na LocalDB-u, 67 testova. Raspakovati u kratku putanju, npr. C:\\RS1\\V06."),
        k(GH6, "Isti kod na GitHubu", "Folder nastava/V06/pocetni, s README-jem: pokretanje, demonstracijski računi, testovi i migracije.")])
      + grupa("2. Market Backend: od MojaApp do startera", [
        k(m, "Mini-prezentacija V06", "Pokretanje i paging, poređenje s MojaApp V05, put zahtjeva, JWT i tri nivoa pristupa, CRUD nad kategorijama sa snimljenim odgovorima, Solution Explorer sa stvarnim kodom (11 slajdova).", "istaknuto")])
      + grupa("3. Vježba u paru", [
        k(m + "#s9", "Opis kategorije kroz sve slojeve", "Novo polje Description: entitet, konfiguracija, migracija, command, validator, handler i DTO, s provjerama u Swaggeru.", "istaknuto")])
      + grupa("4. Priprema za naredni čas", [
        k(m + "#s10", "Narudžba sa stavkama", "Create i update u jednom zahtjevu: izmjena, uklanjanje i dodavanje stavke, neispravna stavka bez djelimičnog snimanja."),
        k(m + "#s11", "Kontrolna lista za novi CRUD", "Redoslijed za svaku novu backend funkcionalnost na starteru.")]))


KOPIRAJ = io.open(os.path.join(REPO, "kopiraj-kod.js"), encoding="utf-8").read()


def stranica(naslov, opis, tijelo, skripta=False):
    js = f"\n<script>\n{KOPIRAJ}</script>" if skripta else ""
    return f"""<!doctype html>
<html lang="bs">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>{naslov}</title>
<meta name="description" content="{opis}">
{CSS}
</head>
<body>
<main>
  <span class="oznaka">Razvoj softvera I · FIT UNMO 2026/27</span>
{tijelo}
  <footer>Razvoj softvera I, Fakultet informacijskih tehnologija, Univerzitet „Džemal Bijedić“ u Mostaru, 2026/27.
  Početni projekat za seminarski rad: <a href="https://github.com/adil-fit-ba/rs1-2026-27">github.com/adil-fit-ba/rs1-2026-27</a>.</footer>
</main>{js}
</body>
</html>
"""


def kod(naslov, tekst, kom="###"):
    """Blok koda; redovi koji počinju s `kom` (komentar u .http) su istaknuti."""
    redovi = []
    for r in tekst.strip("\n").split("\n"):
        e = html.escape(r)
        redovi.append(f'<span class="kom">{e}</span>' if r.startswith(kom) else e)
    return f'  <div class="kod"><div class="jezik">{html.escape(naslov)}</div><pre><code>' + "\n".join(redovi) + "</code></pre></div>\n"


def tabela(redovi):
    g, *t = redovi
    th = "".join(f"<th>{c}</th>" for c in g)
    tr = "".join("<tr>" + "".join(f"<td>{c}</td>" for c in r) + "</tr>" for r in t)
    return f'  <div class="tab-okvir"><table><tr>{th}</tr>{tr}</table></div>\n'


HTTP = """### Samostalna vježba: novi nastavnik (201)
POST {{MojaApp.API_HostAddress}}/nastavnici
Content-Type: application/json

{
  "ime": "Lejla",
  "prezime": "Lejlić",
  "email": "lejla.lejlic@example.com",
  "zvanje": "docent"
}

### Isti email (409)
POST {{MojaApp.API_HostAddress}}/nastavnici
Content-Type: application/json

{
  "ime": "Lejla",
  "prezime": "Druga",
  "email": "lejla.lejlic@example.com"
}

### Prazno ime i neispravan email (400)
POST {{MojaApp.API_HostAddress}}/nastavnici
Content-Type: application/json

{
  "ime": "",
  "prezime": "Test",
  "email": "abc"
}

### Postojeći nastavnik (200): Id iz odgovora na prvi zahtjev
GET {{MojaApp.API_HostAddress}}/nastavnici/1

### Nepostojeći nastavnik (404)
GET {{MojaApp.API_HostAddress}}/nastavnici/999"""

vjezba = (
    '  <h1>Samostalna vježba: entitet Nastavnik</h1>\n'
    '  <p class="siva">Vježbe 4 · nova entity klasa s GetById i Dodaj, po obrascu iz V04 · <a href="../index.html">svi materijali</a></p>\n'
    '  <p class="cilj"><b>Cilj:</b> cijeli put za novi entitet, od modela do provjere u <code>.http</code> fajlu. '
    'Infrastruktura iz V04 se ne mijenja: MediatR sam pronalazi nove handlere (klase koje implementiraju <code>IRequestHandler</code> '
    'u ovom projektu), a <code>AddValidatorsFromAssembly</code> novi validator, pa <code>Program.cs</code> ostaje isti.</p>\n'
    f'  <p><b>Polazna tačka:</b> vlastiti projekat nakon V04 ili <a href="{GH}">rješenje V04</a> (folder <code>nastava/V04/rjesenje</code>).</p>\n'
    '  <p><b>Podsjetnik:</b> command nosi podatke, handler izvršava operaciju, a <code>ISender.Send</code> ih povezuje prema tipu commanda '
    '(<a href="v04-nadogradnja-v03-na-cqrs.html#s3">prezentacija V04, slajdovi 3 i 4</a>).</p>\n'
    '  <h2>Koraci</h2>\n'
    '  <p class="siva">Istim redom kao postupak iz prezentacije V04. Poslije svakog koraka: build.</p>\n'
    + tabela([
        ["#", "Šta", "Fajl"],
        ["1", "Entitet: <code>Id</code>, <code>Ime</code>, <code>Prezime</code>, <code>Email</code>, <code>Zvanje</code> (nije obavezno), <code>DatumKreiranja</code>", "<code>Models/Nastavnik.cs</code>"],
        ["2", "<code>DbSet&lt;Nastavnik&gt; Nastavnici</code> i Fluent API: <code>Ime</code> i <code>Prezime</code> do 50 znakova, <code>Email</code> do 100 znakova i jedinstven indeks, <code>Zvanje</code> do 50 znakova", "<code>Data/AppDbContext.cs</code>"],
        ["3", "Migracija <code>Nastavnici</code>, pa <code>Update-Database</code>", "<code>Migrations/</code>"],
        ["4", "Odgovor <code>NastavnikResponse</code>: <code>Id</code>, <code>ImePrezime</code>, <code>Email</code>, <code>Zvanje</code>", "<code>Dtos/NastavnikResponse.cs</code>"],
        ["5", "Query <code>NastavnikGetByIdQuery</code> i handler: projekcija u <code>Select</code>; ako nastavnik ne postoji, <code>NotFoundException</code>", "<code>Modules/Nastavnici/Queries/GetById/</code>"],
        ["6", "Command <code>DodajNastavnikaCommand</code>, validator i handler: ako email već postoji, <code>ConflictException</code>", "<code>Modules/Nastavnici/Commands/Dodaj/</code>"],
        ["7", "<code>NastavniciController</code> sa <code>ISender</code>: <code>GET /Nastavnici/{id}</code> i <code>POST /Nastavnici</code> (201, <code>CreatedAtAction</code>)", "<code>Controllers/NastavniciController.cs</code>"],
    ])
    + '  <h2>Pravila u validatoru</h2>\n'
    '  <p class="siva">Validator ne pristupa bazi podataka: provjerava samo podatke iz zahtjeva. Jedinstven email provjerava handler (409).</p>\n'
    '  <ul>\n'
    '    <li><code>Ime</code> i <code>Prezime</code>: obavezni, najviše 50 znakova.</li>\n'
    '    <li><code>Email</code>: obavezan, ispravan email (<code>EmailAddress()</code>), najviše 100 znakova.</li>\n'
    '    <li><code>Zvanje</code>: najviše 50 znakova.</li>\n'
    '  </ul>\n'
    '  <h2>Provjera</h2>\n'
    + tabela([
        ["Zahtjev", "Očekivano"],
        ["<code>POST /Nastavnici</code> s ispravnim podacima", "201, zaglavlje <code>Location: /Nastavnici/{id}</code>"],
        ["<code>POST</code> s praznim imenom i emailom <code>abc</code>", "400, greške za <code>Ime</code> i <code>Email</code>"],
        ["<code>POST</code> s emailom koji već postoji", "409"],
        ["<code>GET /Nastavnici/{id}</code>", "200"],
        ["<code>GET /Nastavnici/999</code>", "404"],
    ])
    + '  <p class="siva">Zahtjevi za kraj fajla <code>MojaApp.API.http</code>:</p>\n'
    + kod("MojaApp.API.http", HTTP)
    + '  <h2>Dodatno</h2>\n'
    '  <ul>\n'
    '    <li><code>GET /Nastavnici</code>: lista bez paginga, sortirana po prezimenu.</li>\n'
    '    <li>Veza <code>Predmet</code> → <code>Nastavnik</code>: <code>NastavnikId</code> nije obavezan; nepostojeći nastavnik daje 404 iz handlera, '
    'kao smjer kod studenta.</li>\n'
    '  </ul>\n'
    '  <p class="siva">Rad ide kao u V02 i V03: issue, grana, commit po koraku i pull request.</p>\n'
)
os.makedirs(os.path.join(REPO, "vjezbe-04"), exist_ok=True)
io.open(os.path.join(REPO, "vjezbe-04", "samostalna-vjezba-nastavnik.html"), "w", encoding="utf-8", newline="\n").write(
    stranica("Samostalna vježba: entitet Nastavnik", "RS1 vježbe 4: nova entity klasa Nastavnik s GetById i Dodaj, po obrascu iz V04.", vjezba, skripta=True))
print("vjezbe-04/samostalna-vjezba-nastavnik.html")

HTTP5 = """### Vježba 1: novi nastavnik (201)
POST {{MojaApp.API_HostAddress}}/nastavnici
Content-Type: application/json

{
  "ime": "Lejla",
  "prezime": "Lejlić",
  "email": "lejla.lejlic@example.com",
  "zvanje": "docent"
}

### Isti email (409)
POST {{MojaApp.API_HostAddress}}/nastavnici
Content-Type: application/json

{
  "ime": "Lejla",
  "prezime": "Druga",
  "email": "lejla.lejlic@example.com"
}

### Brisanje (204), pa ponovo GET (404); isti email se zatim može upisati (201)
DELETE {{MojaApp.API_HostAddress}}/nastavnici/1

### Vježba 2: obrisani predmeti
GET {{MojaApp.API_HostAddress}}/predmeti/obrisani

### Vježba 3: vraćanje obrisanog predmeta (204; nije obrisan 404; aktivna ista šifra 409)
PUT {{MojaApp.API_HostAddress}}/predmeti/4/vrati

### Vježba 4: brisanje smjera sa studentima (409)
DELETE {{MojaApp.API_HostAddress}}/smjerovi/1"""

vjezbe5 = (
    '  <h1>Samostalne vježbe: soft delete u četiri projekta</h1>\n'
    '  <p class="siva">Vježbe 5 · četiri zadatka nad rješenjem V05 · <a href="../index.html">svi materijali</a></p>\n'
    '  <p class="cilj"><b>Cilj:</b> isti postupak kao na vježbama, na novim primjerima: svaki dio koda u svom projektu, '
    'soft delete i global query filter rade sami, a ono što filter i indeks ne mogu (povezani zapisi, obrisani zapisi) rješava handler.</p>\n'
    f'  <p><b>Polazna tačka:</b> vlastiti projekat nakon V05 ili <a href="{GH5}">rješenje V05</a> (folder <code>nastava/V05/rjesenje</code>). '
    'Očekivani statusi u tabelama su provjereni na rješenju (LocalDB, 04.11.2026).</p>\n'
    '  <p><b>Podsjetnik:</b> validator ne pristupa bazi podataka; nepostojeći zapis je 404 (<code>NotFoundException</code>), '
    'sukob s postojećim podacima 409 (<code>ConflictException</code>), oboje iz handlera '
    '(<a href="v05-clean-architecture-i-soft-delete.html#s23">prezentacija V05, korak 7</a>).</p>\n'
    '  <h2>Vježba 1: entitet Nastavnik u četiri projekta</h2>\n'
    '  <p class="siva">Kao Nastavnik iz V04, ali s BaseEntity, soft delete-om i brisanjem. Poslije svakog koraka: build.</p>\n'
    + tabela([
        ["#", "Šta", "Projekat i fajl"],
        ["1", "<code>Nastavnik : BaseEntity</code>: <code>Ime</code>, <code>Prezime</code>, <code>Email</code>, <code>Zvanje</code> (nije obavezno)", "Domain: <code>Entities/Nastavnik.cs</code>"],
        ["2", "Konfiguracija: tabela <code>Nastavnici</code>; <code>Ime</code> i <code>Prezime</code> do 50 znakova, <code>Email</code> do 100, <code>Zvanje</code> do 50; jedinstven <code>Email</code> s <code>HasFilter(\"[IsDeleted] = 0\")</code>", "Infrastructure: <code>Database/Configurations/NastavnikConfiguration.cs</code>"],
        ["3", "<code>DbSet&lt;Nastavnik&gt; Nastavnici</code> u interfejsu i u klasi", "Application: <code>IAppDbContext.cs</code>; Infrastructure: <code>AppDbContext.cs</code>"],
        ["4", "Migracija <code>Nastavnici</code>; primjenjuje je <code>MigrateAsync</code> pri pokretanju", "Infrastructure: <code>Database/Migrations/</code>"],
        ["5", "<code>NastavnikResponse</code> (<code>Id</code>, <code>ImePrezime</code>, <code>Email</code>, <code>Zvanje</code>) i <code>NastavnikGetByIdQuery</code> s handlerom (404)", "Application: <code>Dtos/</code>, <code>Modules/Nastavnici/Queries/GetById/</code>"],
        ["6", "<code>DodajNastavnikaCommand</code>, validator i handler: isti email među neobrisanim nastavnicima → 409", "Application: <code>Modules/Nastavnici/Commands/Dodaj/</code>"],
        ["7", "<code>ObrisiNastavnikaCommand</code> i handler: <code>Remove</code> (soft delete); nepostojeći → 404", "Application: <code>Modules/Nastavnici/Commands/Obrisi/</code>"],
        ["8", "<code>NastavniciController</code> sa <code>ISender</code>: <code>GET /Nastavnici/{id}</code>, <code>POST</code> (201), <code>DELETE</code> (204)", "API: <code>Controllers/NastavniciController.cs</code>"],
    ])
    + kod("Package Manager Console", "Add-Migration Nastavnici -Project MojaApp.Infrastructure -StartupProject MojaApp.API -OutputDir Database\\Migrations", kom="#")
    + '  <p class="siva">Validator: <code>Ime</code> i <code>Prezime</code> obavezni, do 50 znakova; <code>Email</code> obavezan, <code>EmailAddress()</code>, do 100; <code>Zvanje</code> do 50.</p>\n'
    + tabela([
        ["Zahtjev", "Očekivano"],
        ["<code>POST /Nastavnici</code> s ispravnim podacima", "201, <code>Location: /Nastavnici/{id}</code>"],
        ["<code>POST</code> s istim emailom", "409"],
        ["<code>POST</code> s praznim imenom i emailom <code>abc</code>", "400, greške za <code>Ime</code> i <code>Email</code>"],
        ["<code>GET /Nastavnici/{id}</code>; <code>GET /Nastavnici/999</code>", "200; 404"],
        ["<code>DELETE /Nastavnici/{id}</code>, pa <code>GET</code> istog Id-a", "204, pa 404"],
        ["<code>DELETE</code> istog Id-a ponovo", "404"],
        ["<code>POST</code> s emailom obrisanog nastavnika", "201 (indeks s filterom)"],
    ])
    + '  <h2>Vježba 2: lista obrisanih predmeta</h2>\n'
    '  <p><code>GET /Predmeti/obrisani</code> vraća obrisane predmete: <code>Id</code>, <code>Naziv</code>, <code>Sifra</code> i <code>ObrisanUtc</code> '
    '(vrijednost <code>ModifiedAtUtc</code>), od najnovijeg. Query <code>ObrisaniPredmetiQuery</code> u <code>Modules/Predmeti/Queries/Obrisani/</code>.</p>\n'
    '  <ul>\n'
    '    <li>Global query filter skriva obrisane, pa upit počinje s <code>db.Predmeti.IgnoreQueryFilters()</code>, a zatim <code>Where(x =&gt; x.IsDeleted)</code>.</li>\n'
    '    <li>Bez <code>IgnoreQueryFilters()</code> lista je uvijek prazna, jer filter već dodaje <code>IsDeleted = 0</code>.</li>\n'
    '    <li>Ruta <code>obrisani</code> ne smeta ruti <code>{id:int}</code>: ograničenje <code>:int</code> ne prihvata tekst.</li>\n'
    '  </ul>\n'
    + tabela([
        ["Zahtjev", "Očekivano"],
        ["<code>GET /Predmeti/obrisani</code> na novoj bazi podataka", "200, <code>[]</code>"],
        ["<code>POST /Predmeti</code>, pa <code>DELETE</code> tog predmeta, pa <code>GET /Predmeti/obrisani</code>", "200, lista s tim predmetom"],
    ])
    + '  <h2>Vježba 3: vraćanje obrisanog predmeta</h2>\n'
    '  <p><code>PUT /Predmeti/{id}/vrati</code>: command <code>VratiPredmetCommand</code> u <code>Modules/Predmeti/Commands/Vrati/</code>. '
    'Handler nalazi predmet s <code>IgnoreQueryFilters()</code> i postavlja <code>IsDeleted = false</code>; '
    '<code>SaveChangesAsync</code> sam upisuje <code>ModifiedAtUtc</code>.</p>\n'
    + tabela([
        ["Zahtjev", "Očekivano"],
        ["<code>PUT /Predmeti/{id}/vrati</code> za obrisan predmet", "204, a zatim <code>GET /Predmeti/{id}</code> → 200"],
        ["isti zahtjev za predmet koji nije obrisan; <code>PUT /Predmeti/999/vrati</code>", "404; 404"],
        ["predmet je obrisan, pa je dodan novi s istom šifrom; zatim <code>vrati</code> za obrisani", "409 (dva aktivna predmeta s istom šifrom nisu dozvoljena)"],
    ])
    + '  <h2>Vježba 4: brisanje smjera</h2>\n'
    '  <p><code>DELETE /Smjerovi/{id}</code>: command <code>ObrisiSmjerCommand</code> u <code>Modules/Smjerovi/Commands/Obrisi/</code>. '
    '<code>OnDelete(Restrict)</code> između studenta i smjera ne djeluje na soft delete (UPDATE), pa handler provjerava studente.</p>\n'
    + tabela([
        ["Zahtjev", "Očekivano"],
        ["<code>DELETE /Smjerovi/1</code> (smjer ima studente)", "409"],
        ["<code>DELETE /Smjerovi/99</code>", "404"],
        ["smjer bez studenata (npr. nakon <code>PUT /Studenti/2</code> bez <code>smjerId</code>), pa <code>DELETE /Smjerovi/2</code>", "204; <code>GET /Smjerovi</code> ga više nema"],
        ["<code>POST /Studenti</code> s <code>smjerId</code> obrisanog smjera", "404 (handler iz V04, filter ga ne vidi)"],
    ])
    + '  <p class="siva">Zahtjevi za kraj fajla <code>MojaApp.API.http</code> (Id-ovi zavise od redoslijeda zahtjeva):</p>\n'
    + kod("MojaApp.API.http", HTTP5)
    + '  <h2>Dodatno</h2>\n'
    '  <ul>\n'
    '    <li><code>ObrisanUtc</code> u odgovoru nema oznaku <code>Z</code>: SQL Server ne čuva vrstu vremena (<code>DateTimeKind</code>). '
    'Starter to rješava pretvaračem u <code>Market.Infrastructure/Database/DatabaseConfiguration.cs</code> (<code>UtcDateTimeConverter</code>); isti pretvarač se može dodati u MojaApp.</li>\n'
    '    <li><code>CreatedAtUtc</code> i <code>ModifiedAtUtc</code> u <code>StudentDetaljiResponse</code>.</li>\n'
    '  </ul>\n'
    '  <p class="siva">Rad ide kao u V02–V04: issue, grana, commit po koraku i pull request.</p>\n'
)
os.makedirs(os.path.join(REPO, "vjezbe-05"), exist_ok=True)
io.open(os.path.join(REPO, "vjezbe-05", "samostalne-vjezbe.html"), "w", encoding="utf-8", newline="\n").write(
    stranica("Samostalne vježbe V05", "RS1 vježbe 5: Nastavnik u četiri projekta, obrisani predmeti, vraćanje obrisanog predmeta i brisanje smjera.", vjezbe5, skripta=True))
print("vjezbe-05/samostalne-vjezbe.html")

uvod = '  <p class="siva">Stranice rade u web pregledniku, bez instalacije. Prezentacije se listaju strelicama ← →; opcija „Čitanje“ prikazuje sve slajdove na jednoj stranici.</p>\n'
tijelo = ('  <h1>Razvoj softvera I: materijali s vježbi</h1>\n' + uvod
          + '  <h2>Vježbe 6 (12.11.2026.): Market Backend starter</h2>\n' + v06("vjezbe-06/")
          + '  <h2>Vježbe 5 (05.11.2026.): Clean Architecture i soft delete</h2>\n' + v05("vjezbe-05/")
          + '  <h2>Vježbe 4 (29.10.2026.): CQRS, MediatR i validacija</h2>\n' + v04("vjezbe-04/"))
io.open(os.path.join(REPO, "index.html"), "w", encoding="utf-8", newline="\n").write(
    stranica("RS1 2026/27: materijali s vježbi", "Prezentacije, živi primjeri i kod s vježbi iz Razvoja softvera I, FIT UNMO 2026/27.", tijelo))
print("index.html")
