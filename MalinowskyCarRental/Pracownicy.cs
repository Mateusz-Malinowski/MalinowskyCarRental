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
    
    public partial class Pracownicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pracownicy()
        {
            this.Bazy = new ObservableCollection<Bazy>();
            this.Wypozyczenia = new ObservableCollection<Wypozyczenia>();
        }
    
        public int id_pracownika { get; set; }
        public Nullable<int> id_bazy { get; set; }
        public string pesel { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public Nullable<System.DateTime> data_urodzenia { get; set; }
        public string nr_telefonu { get; set; }
        public Nullable<System.DateTime> data_zatrudnienia { get; set; }
        public string stanowisko { get; set; }
        public Nullable<short> stawka_godzinowa { get; set; }
        public string kraj { get; set; }
        public string miasto { get; set; }
        public string ulica { get; set; }
        public string numer_domu { get; set; }
        public string numer_lokalu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Bazy> Bazy { get; set; }
        public virtual Bazy Bazy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}