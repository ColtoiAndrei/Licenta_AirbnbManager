namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("PasswordTable")]
    public partial class PasswordTable
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

    }
}
