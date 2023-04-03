﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityWPF.Model;

namespace UniversityWPF.Library.Interfaces
{
    public interface IStudentService
    {
        ObservableCollection<Student> Students { get; set; }
        RelayCommand SaveChangesCommand { get; }
    }
}
