using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafWebPage.Models;
using WafWebPage.ViewModels;

namespace WafWebPage.Services
{
    public interface ISchoolService
    {
        List<School> GetAllSchools();
        void AddSchool(School school);
        School GetSingleSchoolById(int id);
        void UpdateSchool(School newSchool);
        void DeleteSchool(int id);
        List<Teacher> GetTeachersBySchoolId(int schoolId);
        SchoolViewModel SchoolDeletionConfirmation(int id);
        SchoolViewModel SchoolDetails(int id);
        
    }
}
