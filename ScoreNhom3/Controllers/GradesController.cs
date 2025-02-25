using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        ScoreTableContext stc;
        public GradesController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Grade/GetAllGrades")]
        public IActionResult GetAllGrades()
        {
            return Ok(new { data = stc.TblGrades.ToList() });
        }

        [HttpPost]
        [Route("/Grade/InsertGrade")]
        public IActionResult InsertGrade(string studentID, string testID, decimal score, string teacherID, string? comments, DateTime dategraded)
        {
            if (!Guid.TryParse(studentID, out Guid parsedStudentId) ||
                !Guid.TryParse(testID, out Guid parsedTestId) ||
                !Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for StudentID, TestID, or TeacherID." });
            }

            // Validate StudentID existence
            bool studentExists = stc.TblStudents.Any(s => s.StudentId == parsedStudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }

            // Validate TeacherID existence
            bool teacherExists = stc.TblTeachers.Any(t => t.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            // Validate TestID existence
            bool testExists = stc.TblTests.Any(t => t.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }


            TblGrade g = new TblGrade
            {
                GradeId = Guid.NewGuid(),
                StudentId = parsedStudentId,
                TestId = parsedTestId,
                Score = score,
                TeacherId = parsedTeacherId,
                Comments = comments ?? string.Empty, // Handle null comments
                DateGraded = dategraded
            };

            try
            {
                stc.TblGrades.Add(g);
                stc.SaveChanges();
                return Ok(new { g });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the grade.", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("/Grade/UpdateGrade")]
        public IActionResult UpdateGrade(string gradeID, string studentID, string testID, decimal score, string teacherID, string? comments, DateTime dategraded)
        {
            if (!Guid.TryParse(gradeID, out Guid gradeId) ||
                !Guid.TryParse(studentID, out Guid parsedStudentId) ||
                !Guid.TryParse(testID, out Guid parsedTestId) ||
                !Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for one or more parameters." });
            }

            TblGrade? g = stc.TblGrades.Find(gradeId);
            if (g == null)
            {
                return NotFound(new { message = "Grade not found." });
            }

            // Validate StudentID existence
            bool studentExists = stc.TblStudents.Any(s => s.StudentId == parsedStudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }

            // Validate TeacherID existence
            bool teacherExists = stc.TblTeachers.Any(t => t.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            // Validate TestID existence
            bool testExists = stc.TblTests.Any(t => t.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }

            g.StudentId = parsedStudentId;
            g.TestId = parsedTestId;
            g.Score = score;
            g.TeacherId = parsedTeacherId;
            g.Comments = comments ?? string.Empty; // Handle null comments
            g.DateGraded = dategraded;

            try
            {
                stc.TblGrades.Update(g);
                stc.SaveChanges();
                return Ok(new { g });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the grade.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Grade/DeleteGrade")]
        public IActionResult DeleteGrade(string id)
        {
            if (!Guid.TryParse(id, out Guid gradeId))
            {
                return BadRequest(new { message = "Invalid Grade ID format." });
            }

            TblGrade? g = stc.TblGrades.Find(gradeId);
            if (g == null)
            {
                return NotFound(new { message = "Grade not found." });
            }

            try
            {
                stc.TblGrades.Remove(g);
                stc.SaveChanges();
                return Ok(new { message = "Grade deleted successfully.", deletedGrade = g });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the grade.", error = ex.Message });
            }
        }
    }
}
