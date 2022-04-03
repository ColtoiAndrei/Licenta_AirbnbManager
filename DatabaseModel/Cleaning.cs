namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cleaning")]
    public partial class Cleaning
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        
        public int CompanyId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public decimal? Price { get; set; }
    }
}
