using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        ScoreTableContext stc;
        public StudentsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Student/GetAllStudents")]
        public IActionResult GetAllAdmins()
        {
            return Ok(new { data = stc.TblStudents.ToList() });
        }

        [HttpGet]
        [Route("/Student/GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(new { data = stc.TblStudents.Find(new Guid(id)) });
        }

        [HttpPost]
        [Route("/Student/InsertStudent")]

        public IActionResult InsertStudent(string FName, string LName, string email, string password,DateOnly Dob,string phone,string address)
        {
            TblStudent st = new TblStudent();
            st.StudentId = System.Guid.NewGuid();
            st.FirstName = FName;
            st.LastName = LName;
            st.Email = email;
            st.Password = password;
            st.DateOfBirth = Dob;
            st.PhoneNumber = phone;
            st.Address = address;

            stc.TblStudents.Add(st);
            stc.SaveChanges();
            return Ok(new { st });

        }

        [HttpPut]
        [Route("/Student/UpdateStudent")]

        public IActionResult UpdateStudent(string id, string FName, string LName, string email, string password, DateOnly Dob, string phone, string address)
        {
            TblStudent st = new TblStudent();
            st.StudentId = new Guid(id);
            st.FirstName = FName;
            st.LastName = LName;
            st.Email = email;
            st.Password = password;
            st.DateOfBirth = Dob;
            st.PhoneNumber = phone;
            st.Address = address;

            stc.TblStudents.Update(st);
            stc.SaveChanges();
            return Ok(new { st });

        }

        [HttpDelete]
        [Route("/Student/DeleteStudent")]

        public IActionResult DeleteStudent(string id)
        {
            TblStudent st = new TblStudent();
            st.StudentId = new Guid(id);

            stc.TblStudents.Remove(st);
            stc.SaveChanges();

            return Ok(new { st });

        }
    }
}
