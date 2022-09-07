namespace Infrastructure.Authorization
{
    public class PermissionDefinition
    {
        public const string UserPermissionProvider = "U";
        public const string RolePermissionProvider = "R";
        public const string ClientPermissionProvider = "C";

        public PermissionDefinition(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string DisplayName { get; set; }
    }
}
