using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forumists4.Data;
using Forumists4.Models;
using Microsoft.AspNetCore.Identity;
using Forumists4.Areas.Identity.Data;
using System.Security.Claims;

namespace Forumists4.Areas.User.Controllers
{
    [Area("User")]
    public class UserAllPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserAllPostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User/UserAllPosts
        public async Task<IActionResult> Index()
        {
            List<Posts> posts = await _context.Posts.Include("Creator").OrderByDescending(x => x.CreatedDate).ToListAsync();
            //List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            //foreach (var post in posts)
            //{
            //    foreach (var user in users)
            //    {
            //       if(user.Id == post.Creator.Id)
            //        {
            //            post.Creator = user;
            //        }
            //    }
            //}

              return _context.Posts != null ? 
                          View(posts) :
                          Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        // GET: User/UserAllPosts/Details/5
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

        // GET: User/UserAllPosts/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: User/UserAllPosts/Create
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

        //// GET: User/UserAllPosts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Posts == null)
        //    {
        //        return NotFound();
        //    }

        //    var posts = await _context.Posts.FindAsync(id);
        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(posts);
        //}

        //// POST: User/UserAllPosts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedDate")] Posts posts)
        //{
        //    if (id != posts.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(posts);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostsExists(posts.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(posts);
        //}

        //// GET: User/UserAllPosts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Posts == null)
        //    {
        //        return NotFound();
        //    }

        //    var posts = await _context.Posts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (posts == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(posts);
        //}

        //// POST: User/UserAllPosts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Posts == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        //    }
        //    var posts = await _context.Posts.FindAsync(id);
        //    if (posts != null)
        //    {
        //        _context.Posts.Remove(posts);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PostsExists(int id)
        //{
        //  return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(Comments comment)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Find(currentUserId);
            Posts post = _context.Posts.Find(comment.Post.Id);
            comment.Creator = currentUser;
            comment.Post = post;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details),new {id = comment.Post.Id});
            return View();
        }
    }
    //public class PostsAndComments
    //{
    //    public Models.Comments Comments { get; set; }
    //    public Models.Posts Posts { get; set; }
    //}
}
