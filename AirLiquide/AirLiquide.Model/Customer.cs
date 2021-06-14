using FluentValidation;
using System;

namespace AirLiquide.Model
{
    public class Customer
    {
        public Customer() { }
        public Customer(Guid id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public Guid Id { get; private set; }
        public string Name {get; private set; }
        public int Age { get; private set; }
    }
}
