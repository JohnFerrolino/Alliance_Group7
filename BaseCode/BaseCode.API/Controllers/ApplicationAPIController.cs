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
    public class ApplicationAPIController : ControllerBase
    {
        private readonly IApplicationService _aplService;
        private readonly IMapper _mapper;

        public ApplicationAPIController(IApplicationService aplService, IMapper mapper)
        {
            _aplService = aplService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("getApplication")]
        public HttpResponseMessage GetApplication(int id)
        {
            var apl = _aplService.Find(id);
            return apl != null ? Helper.ComposeResponse(HttpStatusCode.OK, apl) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Application.ApplicationDoesNotExists);
        }

        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetApplicationList([FromQuery] ApplicationSearchViewModel searchModel)
        {
            var responseData = _aplService.FindApplication(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostApplication(ApplicationViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var apl = _mapper.Map<Application>(aplModel);
                var validationErrors = new ApplicationHandler(_aplService).CanAdd(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _aplService.Create(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpPut]
        [ActionName("edit")]
        public HttpResponseMessage PutApplication(ApplicationViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var apl = _mapper.Map<Application>(aplModel);
                var validationErrors = new ApplicationHandler(_aplService).CanUpdate(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _aplService.Update(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessEdit);
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
        public HttpResponseMessage SoftDeleteApplication(ApplicationViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var apl = _mapper.Map<Application>(aplModel);
                var validationErrors = new ApplicationHandler(_aplService).CanUpdate(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _aplService.SoftDelete(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [HttpDelete]
        [ActionName("delete")]
        public HttpResponseMessage DeleteApplication(int id)
        {
            try
            {
                var validationErrors = new ApplicationHandler(_aplService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _aplService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Application.ApplicationSuccessDelete);
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