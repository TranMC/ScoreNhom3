using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ScoreNhom3.ModelScore;

public partial class ScoreTableContext : DbContext
{
    public ScoreTableContext()
    {
    }

    public ScoreTableContext(DbContextOptions<ScoreTableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdmin> TblAdmins { get; set; }

    public virtual DbSet<TblAttendance> TblAttendances { get; set; }

    public virtual DbSet<TblClass> TblClasses { get; set; }

    public virtual DbSet<TblGrade> TblGrades { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblSubject> TblSubjects { get; set; }

    public virtual DbSet<TblTeacher> TblTeachers { get; set; }

    public virtual DbSet<TblTest> TblTests { get; set; }

    
}
