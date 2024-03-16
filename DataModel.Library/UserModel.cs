using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Library
{
   public class UserModel
    {

        public int UserId { get; set; }

        [Display(Name ="Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "User Role")]
        public int UserStatusId { get; set; }
        public UserStatus Status { get; set; }
        [Display(Name = "Date Of Birth")]
         //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [DataType(DataType.Date)]
       
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
