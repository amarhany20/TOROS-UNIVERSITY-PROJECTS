using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forumists4.Data;
using Forumists4.Models;
using Microsoft.AspNetCore.Authorization;

namespace Forumists4.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminManagePostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminManagePostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminManagePosts
        public async Task<IActionResult> Index()
        {
              return _context.Posts != null ? 
                          View(await _context.Posts.Include("Creator").ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        // GET: Admin/AdminManagePosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include("Creator")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var comments = await _context.Comments.Where(e => e.Post.Id == post.Id).Include("Creator")
                .ToListAsync();
            ViewData["Post"] = post;
            ViewData["Comments"] = comments;

            return View();
        }

        // GET: Admin/AdminManagePosts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/AdminManagePosts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Content,CreatedDate")] Posts posts)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(posts);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(posts);
        //}

        // GET: Admin/AdminManagePosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }

        // POST: Admin/AdminManagePosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedDate")] Posts posts)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: Admin/AdminManagePosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Admin/AdminManagePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var posts = await _context.Posts.FindAsync(id);
            if (posts != null)
            {
                _context.Posts.Remove(posts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
          return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id,int postId)
        {
            if (_context.Comments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            
            Comments comment =  _context.Comments.Include("Post").FirstOrDefault(m=> m.Id == id);
            if(comment == null) {
                return RedirectToAction(nameof(Details), new { id = postId });
            }
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details),new {id = postId});
        }
    }
}
