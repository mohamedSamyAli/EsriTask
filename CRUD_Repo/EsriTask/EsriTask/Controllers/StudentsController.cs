using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TSModel;
using BusinessLogic;

namespace EsriTask.Controllers
{
    public class StudentsController : ApiController
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: api/Students
        public IQueryable<Student> GetStudents()
        {
            return unitOfWork.StudentManger.GetAll();
        }
        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            Student student = unitOfWork.StudentManger.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(unitOfWork.StudentManger.Update(student));
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public IHttpActionResult PostStudent( [FromBody]Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(unitOfWork.StudentManger.Insert(student));
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(int id)
        {           
            return Ok(unitOfWork.StudentManger.Remove(id));
        }
    }
}