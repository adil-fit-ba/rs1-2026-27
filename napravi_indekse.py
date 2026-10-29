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

uvod = '  <p class="siva">Stranice rade u web pregledniku, bez instalacije. Prezentacije se listaju strelicama ← →; opcija „Čitanje“ prikazuje sve slajdove na jednoj stranici.</p>\n'
tijelo = ('  <h1>Razvoj softvera I: materijali s vježbi</h1>\n' + uvod
          + '  <h2>Vježbe 4 (29.10.2026.): CQRS, MediatR i validacija</h2>\n' + v04("vjezbe-04/"))
io.open(os.path.join(REPO, "index.html"), "w", encoding="utf-8", newline="\n").write(
    stranica("RS1 2026/27: materijali s vježbi", "Prezentacije, živi primjeri i kod s vježbi iz Razvoja softvera I, FIT UNMO 2026/27.", tijelo))
print("index.html")
