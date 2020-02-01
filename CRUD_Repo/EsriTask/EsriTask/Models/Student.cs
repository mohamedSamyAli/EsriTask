namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int Teatcher_Id { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
