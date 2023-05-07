using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Constants = BaseCode.Data.Constants;

namespace BaseCode.API.Controllers
{
    //[Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PositionAPIController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionAPIController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getPosition")]
        public HttpResponseMessage GetPosition(int id)
        {
            var status = _positionService.Find(id);
            return status != null ? Helper.ComposeResponse(HttpStatusCode.OK, status) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Position.PositionDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Student records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Student records</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetPositionList([FromQuery] PositionSearchViewModel searchModel)
        {
            var responseData = _positionService.FindPosition(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostPosition(PositionViewModel positionModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var position = _mapper.Map<Position>(positionModel);
                var validationErrors = new PositionHandler(_positionService).CanAdd(position);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _positionService.Create(position);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Position.PositionSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function updates a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage PutPosition(PositionViewModel positionModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var position = _mapper.Map<Position>(positionModel);
                var validationErrors = new PositionHandler(_positionService).CanUpdate(position);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _positionService.Update(position);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Position.PositionSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpPut]
        [ActionName("softdelete")]
        public HttpResponseMessage SoftDeletePosition(PositionViewModel positionModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var position = _mapper.Map<Position>(positionModel);
                var validationErrors = new PositionHandler(_positionService).CanUpdate(position);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _positionService.SoftDelete(position);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Position.PositionSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeletePosition(int id)
        {
            try
            {
                var validationErrors = new PositionHandler(_positionService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _positionService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Position.PositionSuccessDelete);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}