using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WafWebPage.Models;

namespace WafWebPage.ViewModels
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
