namespace AirbnbManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PaymentHistory")]
    public partial class PaymentHistory
    {
        [Key]
        public int PaymentId { get; set; }

        public int? CompanyId { get; set; }

        [StringLength(50)]
        public string PropertyCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CleaningDate { get; set; }

        public decimal? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDate { get; set; }
    }
}
