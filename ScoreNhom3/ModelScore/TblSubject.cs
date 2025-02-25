using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblSubjects")]
[Index("SubjectName", Name = "UQ__tblSubje__4C5A7D5584F6A126", IsUnique = true)]
public partial class TblSubject
{
    [Key]
    [Column("SubjectID")]
    public Guid SubjectId { get; set; }

    [StringLength(100)]
    public string SubjectName { get; set; } = null!;

    public string? SubjectDescription { get; set; }

    public int? Credits { get; set; }

    [StringLength(100)]
    public string? Department { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<TblTest> TblTests { get; set; } = new List<TblTest>();
}
