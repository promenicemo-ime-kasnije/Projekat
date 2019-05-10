using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.DataProvider
{
    public class EFCoreDataProvider : IDataProvider
    {
        #region Login

        public async Task<Korisnik> LoginApp(string username, string password)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Korisnik.FirstOrDefaultAsync(a => a.KorisnickoIme == username && a.Lozinka == password);
                return obj;
            }
        }

        public async Task<int> PromeniLozinku(Korisnik korisnik, string novaLozinka)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Korisnik.FirstOrDefaultAsync(a => a.KorisnickoIme == korisnik.KorisnickoIme);
                obj.Lozinka = novaLozinka;
                return await _context.SaveChangesAsync();
            }
        }

        #endregion

        #region Korisnik

        public async Task AddKorisnikAsync(Korisnik korisnik)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Korisnik.Add(korisnik);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteKorisnikAsync(params Korisnik[] korisnici)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var k in korisnici)
                    _context.Korisnik.Remove(k);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateKorisnikAsync(Korisnik korisnik)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Korisnik.FirstOrDefaultAsync(a => a.KorisnickoIme == korisnik.KorisnickoIme);
                obj.Lozinka = korisnik.Lozinka;
                obj.EmailAdresa = korisnik.EmailAdresa;
                obj.VrstaKorisnika = korisnik.VrstaKorisnika;
                obj.Ime = korisnik.Ime;
                obj.Prezime = korisnik.Prezime;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Korisnik> GetKorisnikAsync(string KorisnickoIme)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Korisnik.FindAsync(KorisnickoIme);
            }
        }

        public async Task<IList<Korisnik>> GetKorisniciAsync()
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Korisnik.AsNoTracking().ToListAsync();

            }

        }

        public async Task<int> GetKorisniciCountAsync()
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Korisnik.CountAsync();
            }
        }

        #endregion

        #region Projekat

        public async Task<int> AddProjekatAsync(Projekat projekat)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Projekat.Add(projekat);
                await _context.SaveChangesAsync();

                // Dodaj aktivnost o tome da je kreiran novi projekat
                await AddAktivnostAsync(new Aktivnost { IDProjekta = projekat.IDProjekta, Poruka = $"Kreiran je projekat {projekat.NazivProjekta}" });

                return projekat.IDProjekta;
            }
        }

        public async Task<int> DeleteProjekatAsync(params Projekat[] projekti)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var p in projekti)
                    _context.Projekat.Remove(p);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateProjekatAsync(Projekat projekat)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Projekat.FirstOrDefaultAsync(a => a.IDProjekta == projekat.IDProjekta);
                obj.NazivProjekta = projekat.NazivProjekta;
                obj.VrstaProjekta = projekat.VrstaProjekta;
                obj.StanjeProjekta = projekat.StanjeProjekta;
                obj.DatumPocetka = projekat.DatumPocetka;
                obj.DatumIzgradnje = projekat.DatumIzgradnje;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Projekat> GetProjekatAsync(long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Projekat.FindAsync(IDProjekta);
            }
        }

        public async Task<IList<Projekat>> GetProjekteAsync()
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Projekat.AsNoTracking().ToListAsync();
            }
        }

        public async Task<int> GetProjekatCountAsync()
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Projekat.CountAsync();
            }
        }

        #endregion

        #region Interakcija

        public async Task AddInterakcijaAsync(String korisnickoIme, long IDprojekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                //_context.Korisnik.AsNoTracking().FirstOrDefault(a => a.KorisnickoIme == korisnik.KorisnickoIme).Projekat.Add(projekat);
                await _context.Database.ExecuteSqlCommandAsync("Insert into Interakcija Values({0},{1})", IDprojekta, korisnickoIme);
                await _context.SaveChangesAsync();

                // Dodaj aktivnost o tome da je korisnik dodeljen projektu
                await AddAktivnostAsync(new Aktivnost { IDProjekta = (int)IDprojekta, Poruka = $"{(await GetKorisnikAsync(korisnickoIme)).PunoIme} je dodeljen projektu." });
            }
        }

        public async Task<int> DeleteInterakcijaAsync(String korisnickoIme, long IDprojekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                //_context.Korisnik.FirstOrDefault(a => a.KorisnickoIme == korisnik.KorisnickoIme).Projekat.Remove(projekat);
                await _context.Database.ExecuteSqlCommandAsync("Delete from Interakcija where IDProjekta = {0} and KorisnickoIme = {1}", IDprojekta, korisnickoIme);

                // Dodaj aktivnost o tome da je korisnik uklonjen sa projekta
                await AddAktivnostAsync(new Aktivnost { IDProjekta = (int)IDprojekta, Poruka = $"{(await GetKorisnikAsync(korisnickoIme)).PunoIme} je uklonjen sa projekta." });
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Korisnik>> GetKorisnikeProjektaAsync(long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Korisnik.Where(a => a.Projekat.Any(b => b.IDProjekta == IDProjekta)).ToListAsync();
            }
        }

        public async Task<IList<Projekat>> GetProjekteKorisnikaAsync(string KorisnickoIme)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Projekat.Where(a => a.Korisnik.Any(b => b.KorisnickoIme == KorisnickoIme)).ToListAsync();
            }
        }

        #endregion

        #region Dokumentacija

        public async Task AddDokumentAsync(Dokumentacija dokument)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Dokumentacija.Add(dokument);
                //await AddAktivnostAsync(new Aktivnost { IDProjekta = (int)dokument.IDProjekta, Poruka = $"{korisnik.PunoIme} je prilozio dokument {dokument.Naziv}" });
                await _context.SaveChangesAsync();
            }
        }

        public async Task KreirajProjekatIDodajDokumenta(Projekat projekat, params Dokumentacija[] dokumenti)
        {
            int idProjekta = await AddProjekatAsync(projekat);
            foreach (Dokumentacija d in dokumenti)
                d.IDProjekta = idProjekta;
            await AddDokumentaAsync(dokumenti);

            // Dodaj aktivnost - kreiran je novi projekat
            await AddAktivnostAsync(new Aktivnost { IDProjekta = idProjekta, Poruka = $"Kreiran je projekat {projekat.NazivProjekta}" });
        }

        public async Task AddDokumentaAsync(params Dokumentacija[] dokumenti)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var d in dokumenti)
                    _context.Dokumentacija.Add(d);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteDokumentAsync(params Dokumentacija[] dokumenti)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var d in dokumenti)
                    _context.Dokumentacija.Remove(d);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateDokumentAsync(Dokumentacija dokument)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Dokumentacija.FirstOrDefaultAsync(a => a.IDDokumenta == dokument.IDDokumenta);
                obj.Naziv = dokument.Naziv;
                obj.Datum = dokument.Datum;
                obj.StatusDokumenta = dokument.StatusDokumenta;
                obj.Redosled = dokument.Redosled;
                obj.IDProjekta = obj.IDProjekta;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Dokumentacija> GetDokumentAsync(long IDDokumenta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Dokumentacija.FindAsync(IDDokumenta);
            }
        }

        public async Task<IList<Dokumentacija>> GetDokumentaProjekta(long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Dokumentacija.Where(a => a.IDProjekta == IDProjekta).ToListAsync();
            }
        }


        public async Task<bool> DokumentImaPDFFajl(long IDDokumenta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.PDF.FirstOrDefaultAsync(p => p.IDDokumenta == IDDokumenta) != default(PDF);
            }
        }

        #endregion

        #region PDF

        public async Task<int> AddPDFAsync(PDF pdf, Korisnik korisnik)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.PDF.Add(pdf);
                await _context.SaveChangesAsync();

                // Dodaj aktivnost o tome da je prolozen novi dokument
                var dokument = await GetDokumentAsync(pdf.IDDokumenta);
                await AddAktivnostAsync(new Aktivnost { IDProjekta = (int)dokument.IDProjekta, Poruka = $"{korisnik.PunoIme} je prilozio dokument {dokument.Naziv}" });

                return pdf.IDDokumenta;
            }
        }

        public async Task AddPDFAsync(params PDF[] pdf)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var p in pdf)
                    _context.PDF.Add(p);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeletePDFAsync(params PDF[] pdf)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var p in pdf)
                    _context.PDF.Remove(p);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdatePDFAsync(PDF pdf)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.PDF.FirstOrDefaultAsync(a => a.IDDokumenta == pdf.IDDokumenta);
                obj.PDFFajl = pdf.PDFFajl;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<PDF> GetPDFAsync(long IDDokumenta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
               return await _context.PDF.FindAsync(IDDokumenta);
            }
        }

        #endregion

        #region Aktivnost

        public async Task AddAktivnostAsync(Aktivnost aktivnost)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Aktivnost.Add(aktivnost);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAktivnostiAsync(params Aktivnost[] aktivnosti)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var a in aktivnosti)
                    _context.Aktivnost.Add(a);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAktivnostAsync(params Aktivnost[] aktivnosti)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var a in aktivnosti)
                    _context.Aktivnost.Remove(a);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Aktivnost>> GetAktivnostiProjektaAsync(long IDProjekta, string vrstaKorisnika)
        {
            // TODO: Mora da se uzme u obzir i tip korisnika
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Aktivnost.Where(a => a.IDProjekta == IDProjekta).ToListAsync();
            }
        }


        public async Task<IList<Aktivnost>> GetAktivnostiAsync(string vrstaKorisnika)
        {
            // TODO: Mora da se uzme u obzir i tip korisnika
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Aktivnost.ToListAsync();
            }

        }
            #endregion

            #region Zahtev

            public async Task AddZahtevAsync(Zahtev zahtev)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Zahtev.Add(zahtev);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddZahteveAsync(params Zahtev[] zahtev)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var z in zahtev)
                    _context.Zahtev.Add(z);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteZahtevAsync(params Zahtev[] zahtev)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                foreach (var z in zahtev)
                    _context.Zahtev.Remove(z);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateZahtevAsync(Zahtev zahtev)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                var obj = await _context.Zahtev.FirstOrDefaultAsync(a => a.IDZahteva == zahtev.IDZahteva);
                obj.Naslov = zahtev.Naslov;
                obj.Poruka = zahtev.Poruka;
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Zahtev>> GetZahteveProjektaAsync(long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Zahtev.Where(a => a.IDProjekta == IDProjekta).ToListAsync();
            }
        }

        public async Task<IList<Zahtev>> GetZahtevePosiljaocaAsync(string KorisnickoIme, long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Zahtev.Where(a => a.IDProjekta == IDProjekta && a.KorisnickoImePosiljaoca == KorisnickoIme).ToListAsync();
            }
        }

        public async Task<IList<Zahtev>> GetZahtevePrimaocaAsync(string KorisnickoIme, long IDProjekta)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                return await _context.Zahtev.Where(a => a.IDProjekta == IDProjekta && a.KorisnickoImePrimaoca == KorisnickoIme).ToListAsync();
            }
        }

        #endregion
    }
}
