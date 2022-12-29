using Clay.Domain.Core.DomainObjects;
using System.Collections.Immutable;

namespace Clay.Domain
{
    public class Lock : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public LockType Type { get; set; }
        private IReadOnlyList<Role> AllowedRoles { get; set; }
        private History History { get; set; }

        public void Unlock(Role userRole)
        {
            if (!CanUnlock(userRole))
                throw new InvalidOperationException("");
        }

        public bool CanUnlock(Role userRole)
        {
            return AllowedRoles.Contains(userRole);
        }

        public bool IsLocked()
        {
            return true;
        }

        public IReadOnlyList<Log> GetLogs()
        {
            return History.GetLogs();
        }
    }
}