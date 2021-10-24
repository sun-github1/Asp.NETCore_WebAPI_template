using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.API.Controllers
{
    public class ErrorController : ControllerBase
    {

        private readonly ILogger<ErrorController> logger;

        // Inject ASP.NET Core ILogger service. Specify the Controller
        // Type as the generic parameter. This helps us identify later
        // which class or controller has logged the exception
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [AllowAnonymous]
        [Route("Error")]
        [HttpGet]
        public ActionResult Error()
        {

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            // LogError() method logs the exception under Error category in the log

            logger.LogError($"The path {exceptionHandlerPathFeature.Path} " +
                $"threw an exception {exceptionHandlerPathFeature.Error}");
            var exception = exceptionHandlerPathFeature.Error;
           
            //return Problem(
            //       detail: context.Error.StackTrace,
            //       title: context.Error.Message);

            var error=new ErrorDetails()
            {
                StatusCode = 500,
                Message = exceptionHandlerPathFeature.Error.Message
            }.ToString();

            return new ObjectResult(error);
        }

        [Route("Error/{statusCode}")]
        [HttpGet]
        public ActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            var error = "";
            switch (statusCode)
            {
                case 404:
                    //ViewBag.ErrorMessage = "Sorry, the resource could not be found";
                    error = new ErrorDetails()
                    {
                        StatusCode = 404,
                        Message = "Sorry, the resource could not be found"
                    }.ToString();
                    //// LogWarning() method logs the message under
                    //// Warning category in the log
                    logger.LogWarning($"404 error occured. Path = " +
                        $"{statusCodeResult.OriginalPath} and QueryString = " +
                        $"{statusCodeResult.OriginalQueryString}");
                    break;
            }

            return new ObjectResult(error);
        }
    }
}