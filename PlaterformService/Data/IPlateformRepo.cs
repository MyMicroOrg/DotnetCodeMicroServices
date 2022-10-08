
namespace PlatefromService.Models
{
    public interface IPlateformRepo
    {
        bool SaveChanges();
        IEnumerable<Plateform> GetAllPlateforms();
        Plateform GetPlateformById(int id);
        void CreatePalteform(Plateform plateform);
    }
}