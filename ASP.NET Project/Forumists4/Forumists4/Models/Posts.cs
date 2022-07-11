using Forumists4.Areas.Identity.Data;

namespace Forumists4.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual ApplicationUser? Creator { get; set; } 
        
    }
}
