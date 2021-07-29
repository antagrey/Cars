using System;
using Cars.Domain.Contracts;

namespace Cars.Domain
{
    public sealed class Car : Entity
    {
        private Car()
        {

        }

        public Car(AddCarCommand command) : this()
        {
            CreateDate = DateTime.UtcNow;
            Make = command.Make;
            Model = command.Model;
            Colour = command.Colour;
            Year = command.Year;
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Colour { get; private set; }
        public int Year { get; private set; }

        public void Change(ChangeCarCommand command)
        {
            Make = command.Make;
            Model = command.Model;
            Colour = command.Colour;
            Year = command.Year;
        }

    }
}
