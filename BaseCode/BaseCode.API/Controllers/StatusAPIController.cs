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
    public class StatusAPIController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        public StatusAPIController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getStatus")]
        public HttpResponseMessage GetStatus(int id)
        {
            var status = _statusService.Find(id);
            return status != null ? Helper.ComposeResponse(HttpStatusCode.OK, status) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Status.StatusDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Student records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Student records</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetStatusList([FromQuery] StatusSearchViewModel searchModel)
        {
            var responseData = _statusService.FindStatus(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostStatus(StatusViewModel statusModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var status = _mapper.Map<Status>(statusModel);
                var validationErrors = new StatusHandler(_statusService).CanAdd(status);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _statusService.Create(status);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Status.StatusSuccessAdd);
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
        public HttpResponseMessage PutStatus(StatusViewModel statusModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var status = _mapper.Map<Status>(statusModel);
                var validationErrors = new StatusHandler(_statusService).CanUpdate(status);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _statusService.Update(status);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Status.StatusSuccessEdit);
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
        public HttpResponseMessage SoftDeleteStatus(StatusViewModel statusModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var status = _mapper.Map<Status>(statusModel);
                var validationErrors = new StatusHandler(_statusService).CanUpdate(status);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _statusService.SoftDelete(status);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Status.StatusSuccessEdit);
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
        public HttpResponseMessage DeleteStatus(int id)
        {
            try
            {
                var validationErrors = new StatusHandler(_statusService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _statusService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Status.StatusSuccessDelete);
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