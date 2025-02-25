using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreNhom3.ModelScore;

namespace ScoreNhom3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        ScoreTableContext stc;
        public AdminsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Admin/GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            return Ok(new { data = stc.TblAdmins.ToList() });
        }

        [HttpPost]
        [Route("/Admin/InsertAdmin")]

        public IActionResult InsertTeacher(string FName, string LName, string email, string password)
        {
            TblAdmin ad = new TblAdmin();
            ad.AdminId= System.Guid.NewGuid();
            ad.FirstName = FName;
            ad.LastName = LName;
            ad.Email = email;
            ad.Password = password;
           
            stc.TblAdmins.Add(ad);
            stc.SaveChanges();
            return Ok(new { ad });

        }

        [HttpPut]
        [Route("/Admin/UpdateAdmin")]

        public IActionResult UpdateAdmin(string id, string FName, string LName, string email, string password)
        {
            TblAdmin ad = new TblAdmin();
            ad.AdminId = new Guid(id);
            ad.FirstName = FName;
            ad.LastName = LName;
            ad.Email = email;
            ad.Password = password;

            stc.TblAdmins.Update(ad);
            stc.SaveChanges();
            return Ok(new { ad });

        }

        [HttpDelete]
        [Route("/Admin/DeleteAdmin")]

        public IActionResult DeleteAdmin(string id)
        {
            TblAdmin ad = new TblAdmin();
            ad.AdminId = new Guid(id);

           stc.TblAdmins.Remove(ad);
            stc.SaveChanges();

            return Ok(new { ad });

        }
    }
}
