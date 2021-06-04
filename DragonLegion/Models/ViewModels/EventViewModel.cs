using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLegion.Models.ViewModels
{
    public class EventViewModel
    {
        public long Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

        public string Location { get; set; }
    }
}
