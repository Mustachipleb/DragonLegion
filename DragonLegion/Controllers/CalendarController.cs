using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DragonLegion.Data;
using DragonLegion.Models;
using DragonLegion.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace DragonLegion.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<CalendarController> _logger;
        private readonly IMapper mapper;

        public CalendarController(ApplicationDbContext dbContext, ILogger<CalendarController> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            _logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
