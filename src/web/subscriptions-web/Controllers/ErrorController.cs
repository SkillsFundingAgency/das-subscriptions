using Esfa.Recruit.Employer.Web.ViewModels.Error;
using Esfa.Recruit.Subscriptions.Web.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Esfa.Recruit.Subscriptions.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("error/{id?}")]
        public IActionResult Error(int id)
        {
            ViewBag.IsErrorPage = true; // Used by layout to show/hide elements.

            switch (id)
            {
                case 403:
                    return AccessDenied();
                case 404:
                    return PageNotFound();
                default:
                    break;
            }

            return View(new ErrorViewModel { StatusCode = id, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/handle")]
        public IActionResult ErrorHandler()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                string routeWhereExceptionOccurred = exceptionFeature.Path;
                var exception = exceptionFeature.Error;

                if (exception is AggregateException aggregateException)
                {
                    var flattenedExceptions = aggregateException.Flatten();
                    _logger.LogError(flattenedExceptions, "Aggregate exception on path: {route}", routeWhereExceptionOccurred);

                    exception = flattenedExceptions.InnerExceptions.FirstOrDefault();
                }
            }

            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View(ViewNames.ErrorView, new ErrorViewModel { StatusCode = (int)HttpStatusCode.InternalServerError, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IActionResult AccessDenied()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return View(ViewNames.AccessDenied);
        }

        private IActionResult PageNotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View(ViewNames.PageNotFound);
        }
    }
}