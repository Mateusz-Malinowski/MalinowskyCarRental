//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalinowskyCarRental
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Bazy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bazy()
        {
            this.Pracownicy1 = new ObservableCollection<Pracownicy>();
            this.Samochody = new ObservableCollection<Samochody>();
        }
    
        public int id_bazy { get; set; }
        public Nullable<int> id_pracownika { get; set; }
        public string kraj { get; set; }
        public string miasto { get; set; }
        public string ulica { get; set; }
        public string numer_domu { get; set; }
        public string numer_lokalu { get; set; }
    
        public virtual Pracownicy Pracownicy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Pracownicy> Pracownicy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Samochody> Samochody { get; set; }
    }
}
