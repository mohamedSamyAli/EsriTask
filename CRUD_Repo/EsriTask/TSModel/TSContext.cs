    using TSModel;
    using System;
    using System.Data.Entity;
    using System.Linq;



namespace TSModel
{

    public class TSContext : DbContext
    {
        private static TSContext instance = new TSContext();

        private TSContext()
            : base("name=TSModel")
        {
        }
        public static TSContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TSContext();
                }
                return instance;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Teacher)
                .WithMany(e => e.students)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentRefId");
                    cs.MapRightKey("TeacherRefId");
                    cs.ToTable("StudentTeacher");
                });
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teatchers { get; set; }

    }
}