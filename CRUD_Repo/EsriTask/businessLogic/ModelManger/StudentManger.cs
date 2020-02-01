using TSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace BusinessLogic.ModelManger
{
    public class StudentManger : GenaricRepo<Student, TSContext>
    {
        public StudentManger(TSContext _context) : base(_context)
        {
        }
    }
}
