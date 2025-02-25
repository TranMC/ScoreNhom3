using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblTeachers")]
[Index("Email", Name = "UQ__tblTeach__A9D105344E0E50EE", IsUnique = true)]
public partial class TblTeacher
{
    [Key]
    [Column("TeacherID")]
    public Guid TeacherId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Department { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<TblClass> TblClasses { get; set; } = new List<TblClass>();

    [InverseProperty("Teacher")]
    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();
}
