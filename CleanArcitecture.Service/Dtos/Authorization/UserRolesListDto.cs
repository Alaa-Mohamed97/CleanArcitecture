namespace CleanArcitecture.Service.Dtos
{
    public class UserRolesListDto
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
