// Dugme „Kopiraj“ u svakom bloku koda (.kod s <pre>): mini-prezentacije i HTML prezentacije iz lib.js.
// Tekst se uzima iz innerText, pa ostaju prelomi redova i kad su redovi <span> s display: block.
(function () {
  async function uClipboard(t) {
    try { await navigator.clipboard.writeText(t); return true; }
    catch (e) {
      // rezerva kad Clipboard API nije dostupan (stariji web preglednik, stranica bez HTTPS-a)
      const a = document.createElement("textarea");
      a.value = t; a.setAttribute("readonly", ""); a.style.position = "fixed"; a.style.opacity = "0";
      document.body.append(a); a.select();
      let ok = false; try { ok = document.execCommand("copy"); } catch (e2) { ok = false; }
      a.remove(); return ok;
    }
  }
  function dodaj() {
    document.querySelectorAll(".kod").forEach((k) => {
      const pre = k.querySelector("pre");
      if (!pre || k.querySelector(".kopiraj")) return;
      const b = document.createElement("button");
      b.type = "button"; b.className = "kopiraj"; b.textContent = "Kopiraj"; b.setAttribute("aria-label", "Kopiraj kod");
      b.addEventListener("click", async (e) => {
        e.stopPropagation();
        const t = pre.innerText.replace(/ /g, " ").replace(/[ \t]+$/gm, "").replace(/\n+$/, "");
        const ok = await uClipboard(t);
        b.textContent = ok ? "Kopirano" : "Nije kopirano"; b.classList.toggle("ok", ok);
        clearTimeout(b._t); b._t = setTimeout(() => { b.textContent = "Kopiraj"; b.classList.remove("ok"); }, 1500);
      });
      k.append(b);
    });
  }
  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", dodaj); else dodaj();
})();
