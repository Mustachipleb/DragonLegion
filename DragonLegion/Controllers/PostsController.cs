using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DragonLegion.Data.Repositories;
using DragonLegion.Models;
using DragonLegion.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;

namespace DragonLegion.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> logger;
        private readonly PostRepository postRepository;
        private readonly IMapper mapper;

        public PostsController(ILogger<PostsController> logger, PostRepository postRepository, IMapper mapper)
        {
            this.logger = logger;
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery(Name = "page")] int page = 1)
        {
            ViewData["page"] = page;
            ViewData["isLastPage"] = false;
            
            var posts = await postRepository
                .GetAll();
            
            var models = mapper
                .Map<IEnumerable<Post>, IEnumerable<PostViewModels.Index>>(posts);
            
            var pagedPosts = PagingList
                .Create(models, 10, page);


            if (!pagedPosts.Any())
                return BadRequest();
            
            return View(pagedPosts);
        }

        public async Task<IActionResult> Details(long id)
        {
            return View(mapper.Map<Post, PostViewModels.Detail>(await postRepository.GetBy(id)));
        }

        public async Task<IActionResult> Edit(long id)
        {
            return View(mapper.Map<Post, PostViewModels.Edit>(await postRepository.GetBy(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostViewModels.Edit viewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await postRepository.GetBy(id);
                post.Update(viewModel);
                postRepository.Update(post);
                await postRepository.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id });
            }

            return View(viewModel);
        }
        
        public IActionResult Delete(long id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await postRepository.Delete(id);
            await postRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
