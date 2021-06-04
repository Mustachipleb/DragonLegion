using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonLegion.Models.ViewModels;

namespace DragonLegion.Models
{
    public class Event : Entity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public void Update(EventViewModel viewModel)
        {
            Title = viewModel.Title;
            Description = viewModel.Description;
            StartDate = viewModel.StartDate;
            EndDate = viewModel.EndDate;
            Location = viewModel.Location;
        }
    }
}
