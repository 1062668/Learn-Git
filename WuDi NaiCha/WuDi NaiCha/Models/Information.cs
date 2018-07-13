using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WuDi_NaiCha.Models
{
    public class MainPageModel
    {
        public Infor Infor { get; set; }
        public user user { get; set; }
    }


    public class Infor
    {
        [Required(ErrorMessage = "Last Name is Required")]
        public String LastName { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "Birthday is Required")]
        [RegularExpression(@"(([12]\d{3})[-](0[1-9]|1[0-2])[-](0[1-9]|[12]\d|3[01]))|(([12]\d{3})[/](0[1-9]|1[0-2])[/](0[1-9]|[12]\d|3[01]))|(([12]\d{3})[.](0[1-9]|1[0-2])[.](0[1-9]|[12]\d|3[01]))", ErrorMessage = "Birthday Wrong Format ")]
        public String Birthday { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public String Email { get; set; }
        [Required(ErrorMessage = "PassWord is Required")]
        public String PassWord { get; set; }
        public String CellPhone { get; set; }
        public String City { get; set; }
    }
    
    public class user
    {
        public string userName { get; set; }
        public string passWord { get; set; }
        public override string ToString()
        {
            return "userName=" + this.userName + ",passWord=" + this.passWord;
        }
    }



}