using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLegion.Models.ViewModels
{
    public class ProfileViewModels
    {
        public class Detail
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}
