using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonLegion.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DragonLegion.Models
{
    public class Post : Entity
    {
        public string Title { get; set; }
        
        public string Subtitle { get; set; }
        
        public string Content { get; set; }
        
        public IdentityUser Author { get; set; }
        
        //public IEnumerable<PostEdit> Edits { get; set; }

        public void Update(PostViewModels.Edit viewModel)
        {
            Title = viewModel.Title;
            Subtitle = viewModel.Subtitle;
            Content = viewModel.Content;
        }
    }

    public class PostEdit : Entity
    {
        public IdentityUser Editor { get; set; }
        
        public DateTime EditedOn { get; set; }
        
        public string TitleSnapshot { get; set; }
        
        public string SubtitleSnapshot { get; set; }
        
        public string ContentSnapshot { get; set; }
    }
}
