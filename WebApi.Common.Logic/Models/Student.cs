using System;

namespace WebApi.Common.Logic.Models
{
    public class Student : Person
    {
        public Student()
        {
        }

        public Student(int iD, string name, string surname, string dNI, DateTime birthDate, int age, DateTime registryDate) : base(iD, name, surname, dNI, birthDate, age, registryDate)
        {
        }

        public Student(Guid gUID, int iD, string name, string surname, string dNI, DateTime birthDate, int age, DateTime registryDate) : base(gUID, iD, name, surname, dNI, birthDate, age, registryDate)
        {
        }

        public Student(int iD, string name, string surname, string dNI, DateTime birthDate) : base(iD, name, surname, dNI, birthDate)
        {
        }

        public Student(string guid, string name, string surname, string dni, DateTime birthDate) : base(guid, name, surname, dni, birthDate)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}