"""Pravi početnu stranicu (index.html) za GitHub Pages repozitorija rs1-2026-27, grana gh-pages. Izgled kao MiS 2026/27."""
import io, os, sys
REPO = sys.argv[1] if len(sys.argv) > 1 else os.path.dirname(os.path.abspath(__file__))
CSS = """<style>
:root { --papir: #eef1f4; --list: #fff; --tinta: #17212b; --siva: #56636f; --linija: #d3dae1; --narandza: #b9531a;
  --f-naslov: Cambria, Georgia, serif; --f-tekst: "Segoe UI", "Helvetica Neue", Arial, sans-serif; }
@media (prefers-color-scheme: dark) { :root { --papir: #10161c; --list: #18212a; --tinta: #e3e9ee; --siva: #9ba8b4; --linija: #2a3540; --narandza: #f0894a; color-scheme: dark; } }
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
a { color: var(--narandza); }
footer { margin-top: 2.5rem; font-size: 0.85rem; color: var(--siva); border-top: 1px solid var(--linija); padding-top: 0.8rem; }
</style>"""
k = lambda href, naslov, opis: f'    <a href="{href}"><b>{naslov}</b><span>{opis}</span></a>\n'
grupa = lambda naslov, kartice: f'  <h3>{naslov}</h3>\n  <div class="veze">\n{"".join(kartice)}  </div>\n'


def v04(p):
    return (grupa("Prezentacije", [
        k(p + "v04-nadogradnja-v03-na-cqrs.html", "Nadogradnja V03 na CQRS", "Sedam koraka, komponenta po komponenta: paketi, izuzeci, GlobalExceptionHandler, ValidationBehavior, queries, commands, čišćenje (30 slajdova)."),
        k(p + "v04-nadogradnja-v03-na-cqrs.pdf", "Nadogradnja V03 na CQRS (PDF)", "Ista prezentacija za štampu ili čitanje bez interneta."),
        k(p + "v04-put-zahtjeva-kroz-slojeve.html", "Put zahtjeva kroz slojeve", "Živi primjer: zahtjev prolazi kroz kontroler, validator i handler korak po korak, sa SQL-om i odgovorom.")])
      + grupa("Kod", [
        k(p + "V04-MojaApp-pocetni-kod-dotnet8.zip", "MojaApp: početni kod (ZIP)", "Rješenje vježbi 3, .NET 8. Od njega počinje nadogradnja.")]))


def stranica(naslov, opis, tijelo):
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
</main>
</body>
</html>
"""


uvod = '  <p class="siva">Stranice rade u web pregledniku, bez instalacije. Prezentacije se listaju strelicama ← →; opcija „Čitanje“ prikazuje sve slajdove na jednoj stranici.</p>\n'
tijelo = ('  <h1>Razvoj softvera I: materijali s vježbi</h1>\n' + uvod
          + '  <h2>Vježbe 4 (29.10.2026.): CQRS, MediatR i validacija</h2>\n' + v04("vjezbe-04/"))
io.open(os.path.join(REPO, "index.html"), "w", encoding="utf-8", newline="\n").write(
    stranica("RS1 2026/27: materijali s vježbi", "Prezentacije, živi primjeri i kod s vježbi iz Razvoja softvera I, FIT UNMO 2026/27.", tijelo))
print("index.html")
