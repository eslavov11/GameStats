using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStats.DataAccess;
using GameStats.Models;

namespace GameStats.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class GameCategoryViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string NAME { get; set; }
        [Display(Name = "Game description")]
        public string DESCRIPTION { get; set; }
        [Display(Name = "Category")]
        public string CATEGORY { get; set; }
        public int CATEGORY_ID { get; set; }
    }
}