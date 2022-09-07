using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Authorization
{
    public class PermissionGroupDefinition
    {
        protected internal PermissionGroupDefinition(
            string name)
        {
            Name = name;
            _permissions = new List<PermissionDefinition>();
            _children = new List<PermissionGroupDefinition>();
        }

        private readonly List<PermissionDefinition> _permissions;
        private readonly List<PermissionGroupDefinition> _children;

        public string Name { get; }
        public PermissionGroupDefinition? Parent { get; set; }
        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();
        public IReadOnlyList<PermissionGroupDefinition> Children => _children.ToImmutableList();

        public virtual PermissionDefinition AddPermission(string name)
        {
            var permission = new PermissionDefinition(name);
            _permissions.Add(permission);
            return permission;
        }

        public virtual PermissionGroupDefinition AddChild(PermissionGroupDefinition permissionGroupDefinition)
        {
            permissionGroupDefinition.Parent = this;
            _children.Add(permissionGroupDefinition);
            return permissionGroupDefinition;
        }

        public virtual PermissionGroupDefinition AddChild(string name)
        {
            var permissionGroupDefinition = new PermissionGroupDefinition(name)
            {
                Parent = this,
            };
            permissionGroupDefinition.Parent = this;
            _children.Add(permissionGroupDefinition);
            return permissionGroupDefinition;
        }

        public PermissionDefinition? GetPermissionOrNull([NotNull] string name)
        {
            foreach (var permission in _permissions)
            {
                if (permission.Name == name)
                {
                    return permission;
                }
            }

            return null;
        }
    }
}
