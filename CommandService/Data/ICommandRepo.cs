using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {

        bool SaveChanges();

        //Platform
        IEnumerable<Platform> GetAllPlateforms();
        void CreatePlatform(Platform plat);
        bool PlatformExits(int platformId);
        bool ExternalPatformExists(int externalPlatformId);
        //Command

        IEnumerable<Command> GetCommandsForPlatform(int plateformId);
        Command GetCommand(int plateformId, int commandInd);
        void CreateCommand(int platformId, Command command);


    }
}