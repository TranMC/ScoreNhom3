using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblAttendance")]
public partial class TblAttendance
{
    [Key]
    [Column("AttendanceID")]
    public Guid AttendanceId { get; set; }

    [Column("StudentID")]
    public Guid StudentId { get; set; }

    [Column("TestID")]
    public Guid? TestId { get; set; }

    [StringLength(20)]
    public string AttendanceStatus { get; set; } = null!;

    public DateOnly AttendanceDate { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("TblAttendances")]
    public virtual TblStudent Student { get; set; } = null!;

    [ForeignKey("TestId")]
    [InverseProperty("TblAttendances")]
    public virtual TblTest? Test { get; set; }
}
