using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Cars.Acl.DataMuse.Queries;
using Cars.Acl.DataMuse.Results;
using Cars.Api.Requests;
using Cars.Api.Responses;
using Cars.Application.Command.Results;
using Cars.Application.Query.Queries;
using Cars.Application.Query.Results;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.Query;

namespace Cars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IAsyncQueryHandler<GetCarByIdQuery, CarResult> getCarByIdQueryHandler;
        private readonly IAsyncCommandHandler<AddCarCommand, CarAddedResult> addCarCommandHandler;
        private readonly IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult> removeCarCommandHandler;
        private readonly IAsyncCommandHandler<ChangeCarCommand, CarChangedResult> changeCarCommandHandler;
        private readonly IAsyncQueryHandler<WordsSoundingLikeRequest, SoundsLikeWordResults> getWordsSoundingLikeHandler;

        private readonly ILogger<CarController> _logger;

        public CarController(
            ILogger<CarController> logger,
            IAsyncQueryHandler<GetCarByIdQuery, CarResult> getCarByIdQueryHandler,
            IAsyncCommandHandler<AddCarCommand, CarAddedResult> addCarCommandHandler,
            IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult> removeCarCommandHandler,
            IAsyncCommandHandler<ChangeCarCommand, CarChangedResult> changeCarCommandHandler,
            IAsyncQueryHandler<WordsSoundingLikeRequest, SoundsLikeWordResults> getWordsSoundingLikeHandler)
        {
            _logger = logger;
            this.getCarByIdQueryHandler = getCarByIdQueryHandler;
            this.addCarCommandHandler = addCarCommandHandler;
            this.removeCarCommandHandler = removeCarCommandHandler;
            this.changeCarCommandHandler = changeCarCommandHandler;
            this.getWordsSoundingLikeHandler = getWordsSoundingLikeHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int carId)
        {
            if (carId <= 0)
            {
                return BadRequest();
            }

            var result = await getCarByIdQueryHandler.HandleAsync(
                new GetCarByIdQuery(carId));

            if (result == null)
            {
                return NotFound();
            }

            var soundsLikeResult = await getWordsSoundingLikeHandler.HandleAsync(new WordsSoundingLikeRequest(result.Model));

            var response = new GetCarResponse(result, soundsLikeResult);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCarRequest addCarRequest)
        {
            if (addCarRequest == null)
            {
                return BadRequest();
            }

            await addCarCommandHandler.HandleAsync(
                new AddCarCommand(addCarRequest.Make, addCarRequest.Model, addCarRequest.Colour, addCarRequest.Year)
                );

            var response = new AddCarResponse(addCarCommandHandler.Result);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ChangeCarRequest changeCarRequest)
        {
            if (changeCarRequest == null)
            {
                return BadRequest();
            }

            await changeCarCommandHandler.HandleAsync(
                new ChangeCarCommand(changeCarRequest.CarId, changeCarRequest.Make, changeCarRequest.Model, changeCarRequest.Colour, changeCarRequest.Year)
            );

            if (!changeCarCommandHandler.Result.CarFound)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int carId)
        {
            if (carId <= 0)
            {
                return BadRequest();
            }

            await removeCarCommandHandler.HandleAsync(
                new RemoveCarCommand(carId)
            );

            if (!removeCarCommandHandler.Result.CarFound)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
