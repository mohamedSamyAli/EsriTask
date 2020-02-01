namespace TSModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
  

        public virtual ICollection<Student> students { get; set; }
    }
}
