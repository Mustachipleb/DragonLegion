using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLegion.Models.ViewModels
{
    public class PostViewModels
    {
        public class Index
        {
            public long Id { get; set; }
            
            public string Title { get; set; }
    
            public string Subtitle { get; set; }
    
            public string Content { get; set; }
    
            public string Author { get; set; }
            
            public string AuthorId { get; set; }
            
            public DateTime CreationDate { get; set; }
        }
        
        public class Detail
        {
            public long Id { get; set; }
            
            public string Title { get; set; }
    
            public string Subtitle { get; set; }
    
            public string Content { get; set; }
    
            public string Author { get; set; }
            
            public string AuthorId { get; set; }
            
            public DateTime CreationDate { get; set; }
        }

        public class Edit
        {
            public long Id { get; set; }
            
            [Required]
            [StringLength(200)]
            public string Title { get; set; }
    
            [StringLength(200)]
            public string Subtitle { get; set; }
    
            [Required]
            [StringLength(5000)]
            public string Content { get; set; }
        }

        public class Create
        {
            [Required]
            [StringLength(200)]
            public string Title { get; set; }
    
            [StringLength(200)]
            public string Subtitle { get; set; }
    
            [Required]
            [StringLength(5000)]
            public string Content { get; set; }
        }
    }
}
