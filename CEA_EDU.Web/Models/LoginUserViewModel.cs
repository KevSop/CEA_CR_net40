using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CEA_EDU.Web.Models
{
    public class LoginUserViewModel
    {
        [Display(Name = "账号")]
        public string Code { get; set; }

        public string Name { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        public string Type { get; set; }

        public string Company { get; set; }

        public string UpdateBy { get; set; }
    }
}
