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
    using ClassLibrary.DataProvider;
    using System;
    using System.Collections.Generic;
    
    public partial class Dokumentacija
    {
        public int IDDokumenta { get; set; }
        public string Naziv { get; set; }
        public string Datum { get; set; }
        public Nullable<bool> StatusDokumenta { get; set; }
        public Nullable<int> Redosled { get; set; }
        public Nullable<int> IDProjekta { get; set; }

        public bool IsEnable { get; set; } = false;

        public virtual Projekat Projekat { get; set; }
        public virtual PDF PDF { get; set; }
    }
}
