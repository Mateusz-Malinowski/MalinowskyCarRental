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
    
    public partial class Stany_samochodu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stany_samochodu()
        {
            this.Samochody = new ObservableCollection<Samochody>();
        }
    
        public int id_stanu_samochodu { get; set; }
        public string stan_samochodu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Samochody> Samochody { get; set; }
    }
}