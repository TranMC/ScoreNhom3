using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        ScoreTableContext stc;
        public TestsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Test/GetAllTests")]
        public IActionResult GetAllTests()
        {
            return Ok(new { data = stc.TblTests.ToList() });
        }

        [HttpPost]
        [Route("/Test/InsertTest")]
        public IActionResult InsertTest(string subjectID, string testtype, DateOnly testdate, int duration, decimal maxscore, decimal weightage)
        {

            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }

            // Validate SubjectID existence
            bool subjectExists = stc.TblSubjects.Any(s => s.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }

            TblTest te = new TblTest
            {
                TestId = Guid.NewGuid(),
                SubjectId = parsedSubjectId,
                TestType = testtype,
                TestDate = testdate,
                Duration = duration,
                MaximumScore = maxscore,
                Weightage = weightage
            };
            try
            {
                stc.TblTests.Add(te);
                stc.SaveChanges();
                return Ok(new { te });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the test.", error = ex.Message });

            }
        }

        [HttpPut]
        [Route("/Test/UpdateTest")]
        public IActionResult UpdateTest(string testid, string subjectID, string testtype, DateOnly testdate, int duration, decimal maxscore, decimal weightage)
        {
            if (!Guid.TryParse(testid, out Guid parsedTestId) || !Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for one or more parameters." });
            }
            // Validate TestID existence
            bool testExists = stc.TblTests.Any(t => t.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }
            // Validate SubjectID existence
            bool subjectExists = stc.TblSubjects.Any(s => s.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }
            TblTest te = new TblTest
            {
                TestId = parsedTestId,
                SubjectId = parsedSubjectId,
                TestType = testtype,
                TestDate = testdate,
                Duration = duration,
                MaximumScore = maxscore,
                Weightage = weightage
            };
            try
            {
                stc.TblTests.Update(te);
                stc.SaveChanges();
                return Ok(new { te });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the test.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Test/DeleteTest")]
        public IActionResult DeleteTest(string testid)
        {
            if (!Guid.TryParse(testid, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }
           
            TblTest te = stc.TblTests.Find(parsedTestId);
            if (te == null)
            {
                return NotFound(new { message = "Test not found." });
            }
            try
            {
                stc.TblTests.Remove(te);
                stc.SaveChanges();
                return Ok(new { message = "Test deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the test.", error = ex.Message });
            }
        }
    }
}
