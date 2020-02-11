using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafWebPage.Models;
using WafWebPage.ViewModels;

namespace WafWebPage.Services
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        void AddTeacher(Teacher teacher);
        Teacher GetSingleTeacherById(int id);
        void UpdateTeacher(Teacher newTeacher);
        void DeleteTeacher(int id);
        List<Student> GetStudentsByTeacherId(int teacherId);
        TeacherViewModel TeacherDeletionConfirmation(int id);
        TeacherViewModel TeacherDetails(int id);
        
    }
}
