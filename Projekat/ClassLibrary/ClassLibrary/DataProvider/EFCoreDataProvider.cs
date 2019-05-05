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

        public async Task AddProjekatAsync(Projekat projekat)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Projekat.Add(projekat);
                await _context.SaveChangesAsync();
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

        public async Task AddInterakcijaAsync(Korisnik korisnik, Projekat projekat)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Korisnik.FirstOrDefault(a => a.KorisnickoIme == korisnik.KorisnickoIme).Projekat.Add(projekat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteInterakcijaAsync(Korisnik korisnik, Projekat projekat)
        {
            using (ExtentBazaEntities _context = new ExtentBazaEntities())
            {
                _context.Korisnik.FirstOrDefault(a => a.KorisnickoIme == korisnik.KorisnickoIme).Projekat.Remove(projekat);
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
                obj.PDFFajl = dokument.PDFFajl;
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

        #endregion
    }
}
