//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameStats.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CATEGORY
    {
        public CATEGORY()
        {
            this.GAME_CATEGORY = new HashSet<GAME_CATEGORY>();
        }
    
        public int ID { get; set; }
        [Display(Name = "Category name")]
        public string NAME { get; set; }
    
        public virtual ICollection<GAME_CATEGORY> GAME_CATEGORY { get; set; }
    }
}
