using System;
using System.Collections.Generic;

namespace WebApi.Common.Logic.Models
{
    public abstract class Person : IVuelingObject
    {
        #region Properties

        public int ID { get; set; }
        public Guid GUID { get; set; }        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DNI { get; set; }      
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }        
        public DateTime RegistryDate { get; set; }
        #endregion

        #region Constructors

        protected Person() => GUID = Guid.NewGuid();

        protected Person(Guid gUID, int iD, string name, string surname, string dNI, DateTime birthDate, int age, DateTime registryDate)
        {
            GUID = gUID;
            ID = iD;
            Name = name;
            Surname = surname;
            DNI = dNI;
            BirthDate = birthDate;
            Age = age;
            RegistryDate = registryDate;
        }

        protected Person(int iD, string name, string surname, string dNI, DateTime birthDate, int age, DateTime registryDate)
        {
            GUID = Guid.NewGuid();
            ID = iD;
            Name = name;
            Surname = surname;
            DNI = dNI;
            BirthDate = birthDate;
            Age = age;
            RegistryDate = registryDate;
        }

        protected Person(int iD, string name, string surname, string dNI, DateTime birthDate)
        {
            ID = iD;
            Name = name;
            Surname = surname;
            DNI = dNI;
            BirthDate = birthDate;            
        }

        protected Person(string guid, string name, string surname, string dni, DateTime birthDate)
        {
            GUID = new Guid(guid);
            Name = name;
            Surname = surname;
            DNI = dni;
            BirthDate = birthDate;
        }
        #endregion

        #region Methods

        public void SetGuid()
        {
            GUID = Guid.NewGuid();
        }

        public override int GetHashCode()
        {
            var hashCode = 564319517;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(GUID);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DNI);
            hashCode = hashCode * -1521134295 + BirthDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            hashCode = hashCode * -1521134295 + RegistryDate.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Concat(GUID.ToString(), ",",
                                ID.ToString(), ",",
                                Name, ",",
                                Surname, ",",
                                DNI, ",",
                                BirthDate, ",",
                                Age.ToString(), ",",
                                RegistryDate.ToString());
        }

        public override bool Equals(object obj)
        {
            var persona = obj as Person;
            return persona != null &&
                   GUID.Equals(persona.GUID) &&
                   ID == persona.ID &&
                   Name == persona.Name &&
                   Surname == persona.Surname &&
                   DNI == persona.DNI &&
                   BirthDate.Date == persona.BirthDate.Date &&
                   Age == persona.Age &&
                   RegistryDate.Date == persona.RegistryDate.Date;
        }
        #endregion
    }
}