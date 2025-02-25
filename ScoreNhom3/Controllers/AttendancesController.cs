using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        ScoreTableContext stc;
        public AttendancesController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Attendance/GetAllAttendance")]
        public IActionResult GetAllAttendance()
        {
            return Ok(new { data = stc.TblAttendances.ToList() });
        }

        [HttpPost]
        [Route("/Attendance/InsertAttendance")]
        public IActionResult InsertAttendance(string studentID, string testID, string status, DateOnly attenddate)
        {
            if (!Guid.TryParse(studentID, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid GUID format for StudentID." });
            }

            // Validate StudentID existence
            bool studentExists = stc.TblStudents.Any(s => s.StudentId == parsedStudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }

            if (!Guid.TryParse(testID, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }

            // Validate TestID existence
            bool testExists = stc.TblTests.Any(t => t.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }

            TblAttendance at = new TblAttendance
            {
                AttendanceId = Guid.NewGuid(),
                StudentId = parsedStudentId,
                TestId = parsedTestId,
                AttendanceStatus = status,
                AttendanceDate = attenddate
            };

            try
            {
                stc.TblAttendances.Add(at);
                stc.SaveChanges();
                return Ok(new { at });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the attendance.", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("/Attendance/UpdateAttendance")]
        public IActionResult UpdateAttendance(string attendID, string studentID, string testID, string status, DateOnly attenddate)
        {
            if (!Guid.TryParse(attendID, out Guid AttendanceId)
                || !Guid.TryParse(studentID, out Guid StudentId)
                || !Guid.TryParse(testID, out Guid TestId))
            {
                return BadRequest(new { message = "Invalid GUID format for one or more parameters." });
            }

            TblAttendance? at = stc.TblAttendances.Find(AttendanceId);
            if (at == null)
            {
                return NotFound(new { message = "Attendance not found." });
            }

            // Validate StudentID existence
            bool studentExists = stc.TblStudents.Any(s => s.StudentId == StudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }

            // Validate TestID existence
            bool testExists = stc.TblTests.Any(t => t.TestId == TestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }

            at.StudentId = StudentId;
            at.TestId = TestId;
            at.AttendanceStatus = status;
            at.AttendanceDate = attenddate;

            try
            {
                stc.TblAttendances.Update(at);
                stc.SaveChanges();
                return Ok(new { at });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the attendance.", error = ex.Message });
            }
        }
    }
}
