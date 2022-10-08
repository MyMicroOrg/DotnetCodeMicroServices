using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentException(nameof(command));
            }
            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentException(nameof(plat));
            }
            _context.Platfroms.Add(plat);
        }

        public bool ExternalPatformExists(int externalPlatformId)
        {
            return  _context.Platfroms.Any(p => p.ExternalId == externalPlatformId);
        }

        public IEnumerable<Platform> GetAllPlateforms()
        {
            return _context.Platfroms.ToList();
        }

        public Command GetCommand(int plateformId, int commandInd)
        {
            return _context.Commands
            .Where(c => c.PlatformId == plateformId && c.Id == commandInd).FirstOrDefault()!;
        }

        public IEnumerable<Command> GetCommandsForPlatform(int plateformId)
        {
            return _context.Commands
            .Where(x => x.PlatformId == plateformId)
            .OrderBy(c => c.PlatForm.Name);
        }

        public bool PlatformExits(int platformId)
        {
            return _context.Platfroms.Any(x => x.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}