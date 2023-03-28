using System;
using System.Collections.Generic;

namespace UniversityWPF.Model;

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<Group> Groups { get; set; } = new List<Group>();
}
