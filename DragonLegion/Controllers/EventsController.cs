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

namespace DragonLegion.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<EventsController> _logger;
        private readonly IMapper mapper;

        public EventsController(ApplicationDbContext dbContext, ILogger<EventsController> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            _logger = logger;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var events = await dbContext.Events
                .ToListAsync();
            var models = mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            return View(models);
        }

        public async Task<IActionResult> Json([FromQuery(Name = "start")] DateTime startTime, [FromQuery(Name = "end")] DateTime endTime)
        {
            var events = await dbContext.Events
                .Where(e => 
                    e.StartDate.Date >= startTime.Date && 
                    e.EndDate.Date <= endTime.Date)
                .Select(e => new
                {
                    id = e.Id,
                    title = e.Title,
                    description = e.Description,
                    start = e.StartDate.ToString("O"),
                    end = e.EndDate.ToString("O"),
                    location = e.Location
                })
                .ToListAsync();
            return new JsonResult(events);
        }
        
        public IActionResult New()
        {
            return View(new EventViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> New(EventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var @event = mapper.Map<EventViewModel, Event>(viewModel);
                await dbContext.Events.AddAsync(@event);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Calendar");
            }

            return View(viewModel);
        }
        
        public async Task<IActionResult> Update(long id)
        {
            var @event = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
            var model = mapper.Map<Event, EventViewModel>(@event);
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Update(long id, EventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var @event = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
                @event.Update(viewModel);
                dbContext.Events.Update(@event);
                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var @event = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);
            ViewData["id"] = id;
            ViewData["name"] = @event.Title;
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var @event = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);
            dbContext.Events.Remove(@event);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
