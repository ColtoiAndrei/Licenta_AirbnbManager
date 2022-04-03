namespace AirbnbManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Property
    {
        [Key]
        [StringLength(50)]
        public string PropertyCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int? Rooms { get; set; }

        public decimal? Price { get; set; }
    }
}
