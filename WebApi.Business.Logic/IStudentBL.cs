using System;
using System.Collections.Generic;
using WebApi.Common.Logic.Enums;
using WebApi.Common.Logic.Models;

namespace WebApi.Business.Logic
{
    public interface IStudentBL
    {
        Student Add(Student student);
        void SelectFileType(Extension extension);
        int ComputeAge(DateTime fechaCompletaActual, DateTime fechaNacimiento);
        List<Student> GetAll();
        List<Student> GetSingletonInstance();
        List<Student> Filter(string guid, string nombre, string apellidos, string dni, string id, DateTime dtFechaNacimiento, bool fechaNacimientoChecked, string edad, DateTime dtFechaRegistro, bool fechaRegistroChecked);
        List<Student> DeleteByGuid(string guid);
        Student SelectByGuid(string guid);
        void Update(Student student);
    }
}