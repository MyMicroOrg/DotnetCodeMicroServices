using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/plateforms/{plateformId}/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int plateformId)
        {
            Console.WriteLine($"--- Hit GetCommandsForPlatform : {plateformId}");

            if (!_repository.PlatformExits(plateformId))
            {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(plateformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int plateformId, int commandId)
        {
            System.Console.WriteLine($"--- Hit GetCommandForPlatform : {plateformId}");

            if (!_repository.PlatformExits(plateformId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(plateformId, commandId);
            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int plateformId, CommandCreateDto commandCreateDto)
        {

            System.Console.WriteLine("----> Hit CreateCommandForPlatform");

            if (!_repository.PlatformExits(plateformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(plateformId,command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform), new {plateformId = plateformId, commandId = commandReadDto.Id}, commandReadDto);
        }

    }
}