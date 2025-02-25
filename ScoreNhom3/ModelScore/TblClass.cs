using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblClasses")]
public partial class TblClass
{
    [Key]
    [Column("ClassID")]
    public Guid ClassId { get; set; }

    [StringLength(100)]
    public string ClassName { get; set; } = null!;

    [Column("TeacherID")]
    public Guid TeacherId { get; set; }

    [StringLength(100)]
    public string? Schedule { get; set; }

    [ForeignKey("TeacherId")]
    [InverseProperty("TblClasses")]
    public virtual TblTeacher Teacher { get; set; } = null!;
}
