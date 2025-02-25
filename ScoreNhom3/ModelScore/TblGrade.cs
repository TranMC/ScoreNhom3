using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblGrades")]
[Index("StudentId", "TestId", Name = "UQ_StudentTest", IsUnique = true)]
public partial class TblGrade
{
    [Key]
    [Column("GradeID")]
    public Guid GradeId { get; set; }

    [Column("StudentID")]
    public Guid StudentId { get; set; }

    [Column("TestID")]
    public Guid TestId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Score { get; set; }

    [Column("TeacherID")]
    public Guid? TeacherId { get; set; }

    public string? Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateGraded { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("TblGrades")]
    public virtual TblStudent Student { get; set; } = null!;

    [ForeignKey("TeacherId")]
    [InverseProperty("TblGrades")]
    public virtual TblTeacher? Teacher { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("TblGrades")]
    public virtual TblTest Test { get; set; } = null!;
}
