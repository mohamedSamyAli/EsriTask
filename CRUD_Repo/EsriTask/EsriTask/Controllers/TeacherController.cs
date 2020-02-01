using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TSModel;

namespace EsriTask.Controllers
{
    public class TeacherController : ApiController
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: api/Teachers
        public IQueryable<Teacher> GetTeachers()
        {
            return unitOfWork.TeacherManger.GetAll();
        }
        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult GetTeacher(int id)
        {
            Teacher Teacher = unitOfWork.TeacherManger.GetById(id);
            if (Teacher == null)
            {
                return NotFound();
            }
            return Ok(Teacher);
        }
        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacher(Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(unitOfWork.TeacherManger.Update(Teacher));
        }

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult PostTeacher([FromBody]Teacher Teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(unitOfWork.TeacherManger.Insert(Teacher));
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public IHttpActionResult DeleteTeacher(int id)
        {
            return Ok(unitOfWork.TeacherManger.Remove(id));
        }
    }
}
