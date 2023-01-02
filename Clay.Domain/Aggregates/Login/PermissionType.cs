using System.ComponentModel.DataAnnotations;

namespace Clay.Domain.Aggregates.Login
{
    public enum PermissionType
    {
        [Display(Name = "None")]
        None = 1,
        [Display(Name = "Employee")]
        Employee = 2,
        [Display(Name = "FullAccess")]
        FullAccess = 3
    }
}
