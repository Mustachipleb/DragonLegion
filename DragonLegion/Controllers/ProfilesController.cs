using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DragonLegion.Data;
using DragonLegion.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DragonLegion.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProfilesController> logger;

        public ProfilesController(ApplicationDbContext dbContext, IMapper mapper, ILogger<ProfilesController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        /*public IActionResult Index()
        {
            return View();
        }*/

        [Route("Profiles/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return NotFound();

            return View(mapper.Map<IdentityUser, ProfileViewModels.Detail>(user));
        }
    }
}
