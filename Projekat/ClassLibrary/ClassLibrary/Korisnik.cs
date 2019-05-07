//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public partial class Korisnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Korisnik()
        {
            this.Projekat = new HashSet<Projekat>();
        }
    
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string EmailAdresa { get; set; }
        public string VrstaKorisnika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string PunoIme { get => Regex.Replace($"{Ime} {Prezime}", @"\s+", " "); } // vraca ime + prezime u jednom stringu

        public override bool Equals(object obj)
        {
            return KorisnickoIme == (obj as Korisnik)?.KorisnickoIme;
        }

    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Projekat> Projekat { get; set; }
    }
}
