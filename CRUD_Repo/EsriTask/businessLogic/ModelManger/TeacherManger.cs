using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSModel;

namespace BusinessLogic.ModelManger
{
    public class TeacherManger : GenaricRepo<Teacher, TSContext>
    {
        public TeacherManger(TSContext _context) : base(_context)
        {
        }
    }
}
