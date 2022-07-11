using Forumists4.Areas.Identity.Data;
using Forumists4.Data;
using Forumists4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Forumists4.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    public class UserPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public UserPostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User/UserPosts
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Find(currentUserId);
            return _context.Posts != null ?
                        View(await _context.Posts.Where(e => e.Creator == currentUser).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
        }

        // GET: User/UserPosts/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: User/UserPosts/Create
        public IActionResult Create()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var currentUser = _context.Users.Find(userId);
            //Posts post = new Posts();
            //post.Creator = currentUser;
            return View();
        }

        // POST: User/UserPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("Id,Title,Content,CreatedDate")]
        public async Task<IActionResult> Create(Posts posts)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _context.Users.Find(currentUserId);
                posts.Creator = currentUser;
                _context.Add(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: User/UserPosts/Edit/5
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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Find(currentUserId);
            if (_context.Posts.Where(s => s.Id == posts.Id).FirstOrDefault<Posts>().Creator != currentUser)
            {
                return View("~/Views/Shared/NoAccess.cshtml");
            }
            return View(posts);
        }

        // POST: User/UserPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("Id,Title,Content,CreatedDate")]
        public async Task<IActionResult> Edit(int id, Posts posts)
        {
            
            if (id != posts.Id)
            {
                return NotFound();
            }

            //I have 2 ways of checking the user
            //1. I add the creator.id As a hidden input and I check if the id is the same as the signed in one
            //2. I make a context and get the post user and check if they are the same if they are the same then save em

            //var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var currentUser = _context.Users.Find(currentUserId);


            //var currentPost = _context.Posts.Where(s => s.Id == posts.Id).FirstOrDefault<Posts>();

            //if (currentPost.Creator != currentUser)
            //{
            //    return View("~/Views/Shared/NoAccess.cshtml");
            //}


            ////Detaching the tracked context
            //_context.Entry(currentPost).State = EntityState.Detached;
            //These for debugging the entity framework trackers
            //var TrackCount = _context.ChangeTracker.Entries().Count();

            ////If it is still tracked
            //bool tracked = _context.Entry(currentPost).State != EntityState.Detached;
            //if (tracked)
            //{

            //}
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var currentUser = _context.Users.Find(currentUserId);
                    if (posts.Creator.Id != currentUser.Id)
                    {
                        return View("~/Views/Shared/NoAccess.cshtml");
                    }
                    posts.Creator = currentUser;
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

        // GET: User/UserPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);

            if (posts == null)
            {
                return NotFound();
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _context.Users.Find(currentUserId);
            if (_context.Posts.Where(s => s.Id == posts.Id).FirstOrDefault<Posts>().Creator != currentUser)
            {
                return View("~/Views/Shared/NoAccess.cshtml");
            }

            return View(posts);
        }

        // POST: User/UserPosts/Delete/5
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
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _context.Users.Find(currentUserId);
                if (posts.Creator.Id != currentUser.Id)
                {
                    return View("~/Views/Shared/NoAccess.cshtml");
                }
                _context.Posts.Remove(posts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
