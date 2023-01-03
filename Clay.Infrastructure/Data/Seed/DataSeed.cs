using Clay.Domain.Aggregates.Door;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Login;

namespace Clay.Infrastructure.Data.Seed
{
    public class DataSeed
    {
        public async Task SeedAsync(ClayDbContext context)
        {
            if (!context.Set<Login>().Any())
            {
                var mainEntranceDoor = Door.Create("Main Entrance", "Main Entrance door", true, false);
                var storageRoomDoor = Door.Create("Storage Room", "Storage Room door", true, true);
                var managerRole = Role.Create("Manager");
                var directorRole = Role.Create("Director");
                storageRoomDoor.AddRole(managerRole);
                storageRoomDoor.AddRole(directorRole);

                var manager = Employee.Create("Michael", "Manager");
                var director = Employee.Create("Jan", "Director");
                var employee = Employee.Create("John", "Employee");

                var managerLogin = Login.Create("michael@email.com", "123456", manager, "FullAccess");
                var directorLogin = Login.Create("jan@email.com", "123456", director, "FullAccess");
                var employeeLogin = Login.Create("john@email.com", "123456", employee, "Employee");

                context.Set<Role>().AddRange(managerRole, directorRole);
                context.Set<Door>().AddRange(mainEntranceDoor, storageRoomDoor);
                context.Set<Employee>().AddRange(manager, director, employee);
                context.Set<Login>().AddRange(managerLogin, directorLogin, employeeLogin);

                await context.SaveChangesAsync();
            }
        }
    }
}
