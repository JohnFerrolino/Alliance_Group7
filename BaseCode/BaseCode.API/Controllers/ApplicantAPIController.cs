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
using System.Net.Mail;
using System.Security.Claims;

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

                    var fromEmail = "alliancesoftware.g7@gmail.com";
                    var fromPassword = "yiznfgwfjdeqvjyo";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromEmail);
                    message.Subject = "[Alliance Job Application] Application Notice";
                    message.To.Add(new MailAddress(aplModel.EmailAddress));
                    message.Body = $@"
                    <html>
                      <body>
                        <h1>Thank You for Applying to Alliance Software Inc.</h1>
                        <p>Dear {aplModel.FirstName},</p>
                        <p>Thank you for applying for a job position at Alliance Software Inc. We value your interest in our company and the dedication you demonstrated in crafting your application.</p>
                        <p>We have received your application and will assess it in the upcoming days. If your qualifications meet our criteria, we will get in touch with you to arrange an interview.</p>
                        <p>Once again, we appreciate your interest in our organization. If you have any inquiries or apprehensions, feel free to contact us.</p>
                        <p>Best regards,</p>
                        <p>Alliance Admin</p>
                      </body>
                    </html>";
                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromEmail, fromPassword),
                        EnableSsl = true,
                    };

                    smtpClient.Send(message);
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