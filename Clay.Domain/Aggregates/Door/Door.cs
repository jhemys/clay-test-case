﻿using Clay.Domain.Core.DomainObjects;
using Clay.Domain.Validations;
using Clay.Domain.ValueObjects;

namespace Clay.Domain.Aggregates.Door
{
    public class Door : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsLocked { get; protected set; }
        public bool IsAccessRestricted { get; protected set; }
        private IList<Role> AllowedRoles { get; set; }

        private Door() : base() { }

        protected Door(string name, string description, bool isLocked, bool isRestricted) : this()
        {
            Name = name;
            Description = description;
            IsLocked = isLocked;
            IsAccessRestricted = isRestricted;
        }

        public static Door Create(string name, string description, bool isLocked, bool isRestricted)
        {
            Throw.IfArgumentIsNullOrEmpty(name, "The parameter Name is required.");

            return new Door(name, description, isLocked, isRestricted);
        }

        public bool CanBeUnlockedByRole(Role userRole)
        {
            return !IsAccessRestricted || AllowedRoles.Contains(userRole);
        }

        public void SetName(string name)
        {
            Throw.IfArgumentIsNullOrEmpty(name, "The parameter Name is required.");

            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetLocked(bool isLocked)
        {
            IsLocked = isLocked;
        }

        public void SetAccessRestricted(bool isAccessRestricted)
        {
            IsAccessRestricted = isAccessRestricted;
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

            AllowedRoles.Remove(role);
        }

        public void Unlock(Role userRole)
        {
            Throw.IfArgumentIsFalse(IsLocked, "Door is already unlocked.");

            if (!CanBeUnlockedByRole(userRole))
                throw new DomainException("Informed Role is not allowed to unlock this door.");

            SetLocked(false);
        }

        public void Lock()
        {
            Throw.IfArgumentIsTrue(IsLocked, "Door is already locked.");

            SetLocked(true);
        }
    }
}