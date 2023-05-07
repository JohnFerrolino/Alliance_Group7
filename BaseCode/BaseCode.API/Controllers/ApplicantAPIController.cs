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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicantAPIController : ControllerBase
    {
        private readonly IApplicantService _aplService;
        private readonly IMapper _mapper;

        public ApplicantAPIController(IApplicantService aplService, IMapper mapper)
        {
            _aplService = aplService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getApplicant")]
        public HttpResponseMessage GetApplicant(int id)
        {
            var apl = _aplService.Find(id);
            return apl != null ? Helper.ComposeResponse(HttpStatusCode.OK, apl) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Applicant.ApplicantDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Student records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Student records</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        public HttpResponseMessage GetApplicantList([FromQuery] ApplicantSearchViewModel searchModel)
        {
            var responseData = _aplService.FindApplicants(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage PostApplicant(ApplicantViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var apl = _mapper.Map<Applicant>(aplModel);
                var validationErrors = new ApplicantHandler(_aplService).CanAdd(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _aplService.Create(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessAdd);
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
        public HttpResponseMessage PutApplicant(ApplicantViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var apl = _mapper.Map<Applicant>(aplModel);
                var validationErrors = new ApplicantHandler(_aplService).CanUpdate(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _aplService.Update(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessEdit);
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
        public HttpResponseMessage SoftDeleteApplicant(ApplicantViewModel aplModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var apl = _mapper.Map<Applicant>(aplModel);
                var validationErrors = new ApplicantHandler(_aplService).CanUpdate(apl);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {

                    _aplService.SoftDelete(apl);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessDelete);
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
        public HttpResponseMessage DeleteApplicant(int id)
        {
            try
            {
                var validationErrors = new ApplicantHandler(_aplService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _aplService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessDelete);
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