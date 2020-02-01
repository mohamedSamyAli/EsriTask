using BusinessLogic.ModelManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSModel;

namespace BusinessLogic
{
    public class UnitOfWork
    {
        TSContext context = new TSContext();

        public TeacherManger TeacherManger
        {
            get
            {
                return new TeacherManger(context);
            }
        }

        public StudentManger StudentManger
        {
            get
            {
                return new StudentManger(context);
            }
        }



    }
}
