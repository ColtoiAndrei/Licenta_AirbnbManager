namespace AirbnbManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int BookingId { get; set; }

        [StringLength(50)]
        public string PropertyCode { get; set; }

        public int? CustomerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CheckIn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CheckOut { get; set; }

        public decimal? Price { get; set; }
    }
}
