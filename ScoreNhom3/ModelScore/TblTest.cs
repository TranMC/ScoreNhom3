using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblTests")]
public partial class TblTest
{
    [Key]
    [Column("TestID")]
    public Guid TestId { get; set; }

    [Column("SubjectID")]
    public Guid SubjectId { get; set; }

    [StringLength(50)]
    public string TestType { get; set; } = null!;

    public DateOnly? TestDate { get; set; }

    public int? Duration { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? MaximumScore { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Weightage { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("TblTests")]
    public virtual TblSubject Subject { get; set; } = null!;

    [InverseProperty("Test")]
    public virtual ICollection<TblAttendance> TblAttendances { get; set; } = new List<TblAttendance>();

    [InverseProperty("Test")]
    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();
}
