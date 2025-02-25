using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblStudents")]
[Index("Email", Name = "UQ__tblStude__A9D10534F45D3928", IsUnique = true)]
public partial class TblStudent
{
    [Key]
    [Column("StudentID")]
    public Guid StudentId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<TblAttendance> TblAttendances { get; set; } = new List<TblAttendance>();

    [InverseProperty("Student")]
    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();
}
