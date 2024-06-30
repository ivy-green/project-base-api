namespace ProjectBase.Domain.Entities
{
    public class Role : EntityBase
    {
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
