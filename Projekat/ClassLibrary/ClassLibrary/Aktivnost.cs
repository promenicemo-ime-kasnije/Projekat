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
    
    public partial class Aktivnost
    {
        public int IDAktivnosti { get; set; }
        public int IDProjekta { get; set; }
        public Nullable<bool> DozvolaAdmin { get; set; }
        public Nullable<bool> DozvolaProjektanta { get; set; }
        public Nullable<bool> DozvolaInvestitora { get; set; }
        public Nullable<bool> DozvolaEksternogIgraca { get; set; }
        public string Poruka { get; set; }
        public string Datum { get; set; }

        public string NazivProjekta { get; set; }

        public virtual Projekat Projekat { get; set; }
    }
}
