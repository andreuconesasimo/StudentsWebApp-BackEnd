using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Business.Logic;
using WebApi.Common.Logic;
using WebApi.Common.Logic.Enums;
using WebApi.Common.Logic.Models;
using WebApi.Exceptions;

namespace StudentService.Controllers
{
    public class StudentsController : ApiController
    {
        private readonly ILogger logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IStudentBL studentBL;

        public StudentsController(IStudentBL studentBL)
        {
            try
            {
                this.studentBL = studentBL;
                studentBL.SelectFileType(Extension.Redis);
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }

        // GET: api/Students
        [HttpGet]
        public async Task<List<Student>> GetStudents()
        {
            return await Task<List<Student>>.Run(() => ReadAllStudentsAsync().Result);            
        }
        private async Task<List<Student>> ReadAllStudentsAsync()
        {            
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
                return studentBL.GetAll();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        [HttpGet]
        public async Task<Student> GetStudent(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
                return studentBL.SelectByGuid(guid);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }

        // PUT: api/Students/5
        [ResponseType(typeof(Student))]
        [HttpPut]
        public async Task<IHttpActionResult> PutStudent(Student student)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
                studentBL.Update(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }

        // POST: api/Students
        [ResponseType(typeof(Student))]
        [HttpPost]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
                studentBL.Add(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        [HttpDelete]
        public async Task<List<Student>> DeleteStudent(string guid)
        {
            try
            {
                logger.Debug(MethodBase.GetCurrentMethod().DeclaringType.Name);
                return studentBL.DeleteByGuid(guid);                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new PresentationException(ex.Message);
            }
        }
    }
}