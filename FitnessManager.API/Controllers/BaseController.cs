using System.Net;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResponse<TEntity>(BusinessLogicResponse<TEntity> response)
        {
            return response.Result switch
            {
                BusinessLogicResponseResult.UserIsNotAuthorized => Unauthorized(response.ErrorMessage),
                BusinessLogicResponseResult.AccessDenied => StatusCode((int) HttpStatusCode.Forbidden, response.ErrorMessage),
                BusinessLogicResponseResult.ResourceDoesntExist => NotFound(response.ErrorMessage),
                BusinessLogicResponseResult.ConflictOccured => Conflict(response.ErrorMessage),
                BusinessLogicResponseResult.Ok => Ok(response.Value),
                BusinessLogicResponseResult.Created => NoContent(),
                BusinessLogicResponseResult.Deleted => NoContent(),
                BusinessLogicResponseResult.Updated => NoContent(),
                _ => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }
        
        protected IActionResult HandleResponse<TEntity, TDto>(BusinessLogicResponse<TEntity> response)
        {
            return response.Result switch
            {
                BusinessLogicResponseResult.UserIsNotAuthorized => Unauthorized(response.ErrorMessage),
                BusinessLogicResponseResult.AccessDenied => StatusCode((int) HttpStatusCode.Forbidden, response.ErrorMessage),
                BusinessLogicResponseResult.ResourceDoesntExist => NotFound(response.ErrorMessage),
                BusinessLogicResponseResult.ConflictOccured => Conflict(response.ErrorMessage),
                BusinessLogicResponseResult.Ok => Ok(Mapper.Map<TEntity, TDto>(response.Value)),
                BusinessLogicResponseResult.Created => NoContent(),
                BusinessLogicResponseResult.Deleted => NoContent(),
                BusinessLogicResponseResult.Updated => NoContent(),
                _ => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }
    }
}