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
    
    public partial class GeneralniTrosak
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GeneralniTrosak()
        {
            this.Trosak = new HashSet<Trosak>();
        }
    
        public int IDProjekta { get; set; }
        public Nullable<int> UkupnoNovca { get; set; }
        public Nullable<int> BrojUplata { get; set; }
        public string Procenti { get; set; }
    
        public virtual Projekat Projekat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trosak> Trosak { get; set; }
    }
}
