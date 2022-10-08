using PlatefromService.Data;

namespace PlatefromService.Models
{

    public class PlateformRepo : IPlateformRepo
    {
        private readonly AppDbContext _context;

        public PlateformRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreatePalteform(Plateform plateform)
        {
            if(plateform == null){
                throw new ArgumentException(nameof(plateform));
            }
            _context.Plateforms.Add(plateform);
        }

        public IEnumerable<Plateform> GetAllPlateforms()
        {
            return _context.Plateforms.ToList();
        }

        public Plateform GetPlateformById(int id)
        {
            return _context.Plateforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }


}