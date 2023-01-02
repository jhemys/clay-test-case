using Clay.Domain.DomainObjects;
using Clay.Domain.DomainObjects.Exceptions;
using Clay.Domain.Validations;

namespace Clay.Domain.Aggregates.Door
{
    public class Door : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string? Description { get; protected set; }
        public bool IsLocked { get; protected set; }
        public bool IsAccessRestricted { get; protected set; }
        public virtual IList<Role> AllowedRoles { get; protected set; }

        private Door() : base()
        {
            AllowedRoles = new List<Role>();
        }

        protected Door(string name, string? description, bool isLocked, bool isAccessRestricted) : this()
        {
            Name = name;
            Description = description;
            IsLocked = isLocked;
            IsAccessRestricted = isAccessRestricted;
        }

        public static Door Create(string name, string? description, bool isLocked, bool isAccessRestricted)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");

            return new Door(name, description, isLocked, isAccessRestricted);
        }

        public bool CanBeUnlockedByRole(Role userRole)
        {
            return !IsAccessRestricted || AllowedRoles.Contains(userRole);
        }

        public void SetName(string name)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");

            Name = name;
        }

        public void SetDescription(string? description)
        {
            Description = description;
        }

        private void SetLocked(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public void SetAccessRestricted(bool isAccessRestricted)
        {
            IsAccessRestricted = isAccessRestricted;

            if (!isAccessRestricted)
            {
                AllowedRoles.Clear();
            }
        }

        public void AddRole(Role role)
        {
            Throw.IfArgumentIsFalse(IsAccessRestricted, "Roles can only be added/removed to restricted doors.");
            if (AllowedRoles.Any(existingRole => existingRole.Equals(role)))
                throw new DomainException("Informed role already exists for this door.");

            AllowedRoles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            Throw.IfArgumentIsFalse(IsAccessRestricted, "Roles can only be added/removed to restricted doors.");

            if (!AllowedRoles.Any(existingRole => existingRole.Equals(role)))
                throw new DomainException("Informed role does not exist for this door.");

            AllowedRoles.Remove(role);
        }

        public void Unlock(Role userRole)
        {
            Throw.IfArgumentIsFalse(IsLocked, "Door is already unlocked.");

            if (!CanBeUnlockedByRole(userRole))
                throw new DomainActionNotPermittedException("Informed Role is not allowed to unlock this door.");

            SetLocked(false);
        }

        public void Lock()
        {
            Throw.IfArgumentIsTrue(IsLocked, "Door is already locked.");

            SetLocked(true);
        }
    }
}