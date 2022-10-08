using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlateformService.AsyncDataServices;
using PlateformService.Dtos;
using PlateformService.SyncDataService.Http;
using PlatefromService.Models;
using PlaterformService.Dtos;

namespace PlateformService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlateformsController : ControllerBase
    {
        private readonly IPlateformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlateformsController(IPlateformRepo repository,
        IMapper mapper,
        ICommandDataClient commandDataClient,
        IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlateformReadDto>> GetPlateforms()
        {
            var plateformItem = _repository.GetAllPlateforms();
            return Ok(_mapper.Map<IEnumerable<PlateformReadDto>>(plateformItem));
        }

        [HttpGet("{id}", Name = "GetPlateformById")]
        public ActionResult<PlateformReadDto> GetPlateformById(int id)
        {
            var plateformItem = _repository.GetPlateformById(id);
            if (plateformItem != null)
                return Ok(_mapper.Map<PlateformReadDto>(plateformItem));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlateformCreateDto>> CreatePlateform(PlateformCreateDto plateformCreateDto)
        {
            var plateFormModel = _mapper.Map<Plateform>(plateformCreateDto);
            _repository.CreatePalteform(plateFormModel);
            _repository.SaveChanges();

            var plateformDto = _mapper.Map<PlateformReadDto>(plateFormModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlateformToCommand(plateformDto);
            }
            catch (System.Exception)
            {
                Console.WriteLine($"----> Could not send syncronously: {plateformDto}");
            }

            // Send Asyn Message

            try
            {
                var patformPublishDto = _mapper.Map<PlateformPublishDto>(plateformDto);
                patformPublishDto.Event = "Plateform_Published";
                _messageBusClient.PublishNewPlateform(patformPublishDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"----> Could not send Asyncronously: {plateformDto}");
            }

            return CreatedAtRoute(nameof(GetPlateformById), new { Id = plateformDto.Id }, plateformDto);
        }
    }
}