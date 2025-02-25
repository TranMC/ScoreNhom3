using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;
using System.Xml.Linq;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        ScoreTableContext stc;
        public ClassesController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Class/GetAllClasses")]
        public IActionResult GetAllClasses()
        {
            return Ok(new { data = stc.TblClasses.ToList() });
        }

        [HttpGet]
        [Route("/Class/GetClassById")]
        public IActionResult GetClasseByID(string classId)
        {
            return Ok(new { data = stc.TblClasses.Find(new Guid(classId)) });
        }

        [HttpPost]
        [Route("/Class/InsertClass")]
        public IActionResult InsertClass(string classname, string teacherID, string schedule)
        {
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            // Validate TeacherID existence
            bool teacherExists = stc.TblTeachers.Any(t => t.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            TblClass cl = new TblClass
            {
                ClassId = Guid.NewGuid(),
                ClassName = classname,
                TeacherId = parsedTeacherId,
                Schedule = schedule ?? string.Empty // Handle null schedule
            };

            try
            {
                stc.TblClasses.Add(cl);
                stc.SaveChanges();
                return Ok(new { cl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the grade.", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("/Class/UpdateClass")]
        public IActionResult UpdateClass(string classID, string classname, string teacherID, string schedule)
        {
            if (!Guid.TryParse(classID, out Guid ClassId) ||
               !Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for one or more parameters." });
            }

            TblClass? cl = stc.TblClasses.Find(ClassId);
            if (cl == null)
            {
                return NotFound(new { message = "Class not found." });
            }

            // Validate TeacherID existence
            bool teacherExists = stc.TblTeachers.Any(t => t.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            cl.ClassName = classname;
            cl.TeacherId = parsedTeacherId;
            cl.Schedule = schedule ?? string.Empty; // Handle null schedule

            try
            {
                stc.TblClasses.Update(cl);
                stc.SaveChanges();
                return Ok(new { cl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the class.", error = ex.Message });
            }

        }

        [HttpDelete]
        [Route("/Class/DeleteClass")]
        public IActionResult DeleteClass(string classID)
        {
            if (!Guid.TryParse(classID, out Guid ClassId))
            {
                return BadRequest(new { message = "Invalid Class ID format." });
            }

            TblClass? cl = stc.TblClasses.Find(ClassId);
            if (cl == null)
            {
                return NotFound(new { message = "Class not found." });
            }

            try
            {
                stc.TblClasses.Remove(cl);
                stc.SaveChanges();
                return Ok(new { message = "Class deleted successfully.", deletedClass = cl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the grade.", error = ex.Message });
            }
        }
            
    }
}
