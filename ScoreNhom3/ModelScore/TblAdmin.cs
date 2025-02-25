using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

[Table("tblAdmins")]
[Index("Email", Name = "UQ__tblAdmin__A9D1053422727FDF", IsUnique = true)]
public partial class TblAdmin
{
    [Key]
    [Column("AdminID")]
    public Guid AdminId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;
}
