using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        ScoreTableContext stc;
        public SubjectsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }


        [HttpGet]
        [Route("/Subject/GetAllSubject")]
        public IActionResult GetAllSubject()
        {
            return Ok(new { data = stc.TblSubjects.ToList() });
        }

        [HttpPost]
        [Route("/Subject/InsertSubject")]

        public IActionResult InsertSubject(string SName, string Sdescription, int credits, string Department)
        {
            TblSubject sj = new TblSubject();
            sj.SubjectId = System.Guid.NewGuid();
            sj.SubjectName = SName;
            sj.SubjectDescription = Sdescription;
            sj.Credits = credits;
            sj.Department = Department;

            stc.TblSubjects.Add(sj);
            stc.SaveChanges();
            return Ok(new { sj });

        }

        [HttpPut]
        [Route("/Subject/UpdateSubject")]

        public IActionResult UpdateSubject(string id, string SName, string Sdescription, int credits, string Department)
        {
            TblSubject sj = new TblSubject();
            sj.SubjectId = new Guid(id);
            sj.SubjectName = SName;
            sj.SubjectDescription = Sdescription;
            sj.Credits = credits;
            sj.Department = Department;

            stc.TblSubjects.Update(sj);
            stc.SaveChanges();
            return Ok(new { sj });

        }

        [HttpDelete]
        [Route("/Subject/DeleteSubject")]

        public IActionResult DeleteSubject(string id)
        {
            TblSubject sj = new TblSubject();
            sj.SubjectId = new Guid(id);

            stc.TblSubjects.Remove(sj);
            stc.SaveChanges();

            return Ok(new { sj });

        }
    }
}
