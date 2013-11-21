using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kingdom.Web.Areas.Builders.Models.RegionBuilder
{
    public class RegionBuilderViewModel
    {
        [Required]
        [Display(Name = "Number of rows")]
        public int? X { get; set; }

        [Required]
        [Display(Name = "Number of columns")]
        public int? Y { get; set; }

        [Required]
        [Display(Name = "Size of each region")]
        public int? Size { get; set; }
    }
}