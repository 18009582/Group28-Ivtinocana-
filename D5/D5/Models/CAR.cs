//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace D5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR()
        {
            this.CAR_DEFECTS = new HashSet<CAR_DEFECTS>();
            this.CAR_TYPE = new HashSet<CAR_TYPE>();
            this.MECHANIC_JOB = new HashSet<MECHANIC_JOB>();
            this.OFFERS = new HashSet<OFFER>();
        }
    
        public int CAR_ID { get; set; }
        public int SEATS_ID { get; set; }
        public int COLOUR_ID { get; set; }
        public int TRANSMISSION_ID { get; set; }
        public int DOORS_ID { get; set; }
        public int MAKE_ID { get; set; }
        public int CLIENT_ID { get; set; }
        public int STATUS_ID { get; set; }
        public int BOOKING_ID { get; set; }
        public int PURCHASE_ID { get; set; }
        public int FUELTYPE_ID { get; set; }
        public int BOO_BOOKING_ID { get; set; }
        public Nullable<int> YEAR { get; set; }
        public Nullable<int> MILAGE_ { get; set; }
        public Nullable<double> LISTING_PRICE { get; set; }
        public byte[] IMAGE { get; set; }
    
        public virtual BOOKING_FOR_POSSIBLE_PURCHASE BOOKING_FOR_POSSIBLE_PURCHASE { get; set; }
        public virtual CAR_BOOKING CAR_BOOKING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_DEFECTS> CAR_DEFECTS { get; set; }
        public virtual CAR_STATUS CAR_STATUS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAR_TYPE> CAR_TYPE { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual MAKE MAKE { get; set; }
        public virtual CLIENT CLIENT { get; set; }
        public virtual COLOUR COLOUR { get; set; }
        public virtual FUEL_TYPE FUEL_TYPE { get; set; }
        public virtual NUMBER_OF_DOORS NUMBER_OF_DOORS { get; set; }
        public virtual NUMBER_OF_SEATS NUMBER_OF_SEATS { get; set; }
        public virtual TRANSMISSION TRANSMISSION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MECHANIC_JOB> MECHANIC_JOB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OFFER> OFFERS { get; set; }
    }
}
