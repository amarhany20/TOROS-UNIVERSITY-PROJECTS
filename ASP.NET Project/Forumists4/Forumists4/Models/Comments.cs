using Forumists4.Areas.Identity.Data;

namespace Forumists4.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual Posts? Post { get; set; }

        public virtual ApplicationUser? Creator { get; set; }
    }
}
