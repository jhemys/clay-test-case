﻿using Clay.Domain.DomainObjects;
using Clay.Domain.Validations;

namespace Clay.Domain.Aggregates.Employee
{
    public class Employee : Entity, IAggregateRoot
    {
        public string Name { get; protected set; }
        public string Role { get; protected set; }
        public string Password { get; protected set; }
        public string Email { get; protected set; }
        public string? TagIdentification { get; set; }
        private Employee() : base() { }

        protected Employee(string name, string role, string password, string email, string? tagIdentification) : this()
        {
            Name = name;
            Role = role;
            Password = password;
            Email = email;
            TagIdentification = tagIdentification;
        }

        public static Employee Create(string name, string role, string password, string email, string? tagIdentification = null)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");
            Throw.IfArgumentIsNullOrWhitespace(role, "The parameter Role is required.");
            Throw.IfArgumentIsNullOrWhitespace(email, "The parameter Email is required.");
            Throw.IfArgumentIsInvalidEmail(email, "The parameter Email is invalid.");
            Throw.IfArgumentIsNullOrWhitespace(password, "The parameter Password is required.");

            return new Employee(name, role, password, email, tagIdentification);
        }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            Throw.IfAssertArgumentsAreNotEqual(Password, currentPassword, "Current Password is invalid.");
            Throw.IfAssertArgumentsAreEqual(newPassword, currentPassword, "New Password must be different than Current Password.");

            Password = newPassword;
        }

        public void SetName(string name)
        {
            Throw.IfArgumentIsNullOrWhitespace(name, "The parameter Name is required.");

            Name = name;
        }

        public void SetEmail(string email)
        {
            Throw.IfArgumentIsNullOrWhitespace(email, "The parameter Email is required.");

            Email = email;
        }

        public void SetRole(string role)
        {
            Throw.IfArgumentIsNullOrWhitespace(role, "The parameter Role is required.");

            Role = role;
        }
    }
}
