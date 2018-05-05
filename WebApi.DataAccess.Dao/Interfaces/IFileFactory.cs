using WebApi.Common.Logic.Enums;

namespace WebApi.DataAccess.Dao.Interfaces
{
    public interface IFileFactory
    {
        IFileStudent CrearFichero(Extension extension);
    }
}
