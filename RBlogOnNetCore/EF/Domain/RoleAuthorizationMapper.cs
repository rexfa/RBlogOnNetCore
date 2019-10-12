using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("RoleAuthorizationMapper")]
    public class RoleAuthorizationMapper : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int AuthorizationId { get; set; }
        public Authorization Authorization { get; set; }
    }
}
