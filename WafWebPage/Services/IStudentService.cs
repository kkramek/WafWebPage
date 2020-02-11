using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafWebPage.Models;
using WafWebPage.ViewModels;

namespace WafWebPage.Services
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        void AddStudent(Student student);
        Student GetSingleStudentById(int id);
        void UpdateStudent(Student newStudent);
        void DeleteStudent(int id);
        StudentViewModel StudentDeletionConfirmation(int id);
    }
}
