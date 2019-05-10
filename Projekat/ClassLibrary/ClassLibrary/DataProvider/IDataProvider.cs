using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.DataProvider
{
    interface IDataProvider
    {
        Task<Korisnik> LoginApp(String username, String password);
        Task<int> PromeniLozinku(Korisnik korisnik, string novaLozinka);

        Task AddKorisnikAsync(Korisnik korisnik);
        Task<int> DeleteKorisnikAsync(params Korisnik[] korisnici);
        Task<int> UpdateKorisnikAsync(Korisnik korisnik);
        Task<Korisnik> GetKorisnikAsync(String KorisnickoIme);
        Task<IList<Korisnik>> GetKorisniciAsync();
        Task<int> GetKorisniciCountAsync();

        Task<int> AddProjekatAsync(Projekat projekat);
        Task<int> DeleteProjekatAsync(params Projekat[] projekti);
        Task<int> UpdateProjekatAsync(Projekat projekat);
        Task<Projekat> GetProjekatAsync(long IDProjekta);
        Task<IList<Projekat>> GetProjekteAsync();
        Task<int> GetProjekatCountAsync();

        Task AddInterakcijaAsync(String korisnickoIme, long IDprojekta);
        Task<int> DeleteInterakcijaAsync(String korisnickoIme, long IDprojekta);
        Task<IList<Korisnik>> GetKorisnikeProjektaAsync(long IDProjekta); //ova funkcija uzima sve korisnike koji su angazovani na jedan projekat
        Task<IList<Projekat>> GetProjekteKorisnikaAsync(String KorisnickoIme); //ova funkcija uzima sve projekte na kojima je angazovan bio jedan korisnik
        /*Necu pisati funkcije koje ce da prebroje stvari kojima se bave dve prethodne funkcije, zbog toga sto 
        * prethodne funkcije vracaju listu, a lista ima atribut length i samim tim se dobija count ako je potreban */

        Task AddDokumentAsync(Dokumentacija dokument);
        Task KreirajProjekatIDodajDokumenta(Projekat projekat, params Dokumentacija[] dokumenti);
        Task AddDokumentaAsync(params Dokumentacija[] dokumenti);
        Task<int> DeleteDokumentAsync(params Dokumentacija[] dokumenti);
        Task<int> UpdateDokumentAsync(Dokumentacija dokument);
        Task<Dokumentacija> GetDokumentAsync(long IDDokumenta);
        Task<IList<Dokumentacija>> GetDokumentaProjekta(long IDProjekta); //vraca listu dokumenata jednog projekta
        Task<bool> DokumentImaPDFFajl(long IDDokumenta);

        Task<int> AddPDFAsync(PDF pdf);
        Task AddPDFAsync(params PDF[] pdf);
        Task<int> DeletePDFAsync(params PDF[] pdf);
        Task<int> UpdatePDFAsync(PDF pdf);
        Task<PDF> GetPDFAsync(long IDDokumenta);

        Task AddAktivnostAsync(Aktivnost aktivnost);
        Task AddAktivnostiAsync(params Aktivnost[] aktivnosti);
        Task<int> DeleteAktivnostAsync(params Aktivnost[] aktivnosti);
        Task<IList<Aktivnost>> GetAktivnostiProjektaAsync(long IDProjekta);

        Task AddZahtevAsync(Zahtev zahtev);
        Task AddZahteveAsync(params Zahtev[] zahtev);
        Task<int> DeleteZahtevAsync(params Zahtev[] zahtev);
        Task<int> UpdateZahtevAsync(Zahtev zahtev);
        Task<IList<Zahtev>> GetZahteveProjektaAsync(long IDProjekta);
        Task<IList<Zahtev>> GetZahtevePosiljaocaAsync(string KorisnickoIme, long IDProjekta); //ova funkcija vraca sve zahteve koje je ulogovani korisnik poslao sa tog projekta
        Task<IList<Zahtev>> GetZahtevePrimaocaAsync(string KorisnickoIme, long IDProjekta);
    }
}
