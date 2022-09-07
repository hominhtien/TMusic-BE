
namespace Infrastructure.Authorization
{
    public interface IPermissionDefinitionManager
    {
        PermissionDefinition GetOrNull(string name);

        IReadOnlyList<PermissionDefinition> GetPermissions();

        IReadOnlyList<PermissionGroupDefinition> GetGroups();
    }
}
