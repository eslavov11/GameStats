using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GameStats.Models
{
    public class HumanViewModel
    {
        public int ID { get; set; }
        [Display(Name = "First name")]
        public string FIRST_NAME { get; set; }
        [Display(Name = "Second name")]
        public string SECOND_NAME { get; set; }
        [Display(Name = "Last name")]
        public string LAST_NAME { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string EMAIL { get; set; }
        [Display(Name = "Phone number")]
        public string PHONE_NUMBER { get; set; }
        [Display(Name = "Date of birth")]
        public System.DateTime DATE_OF_BIRTH { get; set; }
        [Display(Name = "Picture")]
        public byte[] PICTURE { get; set; }
        [Display(Name = "Picture")]

        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Change photo")]
        public bool change { get; set; }
    }
}