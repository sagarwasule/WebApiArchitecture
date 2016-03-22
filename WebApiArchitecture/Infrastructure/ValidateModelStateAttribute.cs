using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace WebApiArchitecture.Infrastructure
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                string errMessage = string.Empty;

                foreach (ModelState modelState in actionContext.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errMessage += "Error Message : " + error.ErrorMessage;
                        if(error.Exception != null)
                            errMessage += " Exception : " + error.Exception.Message;
                    }
                }

                actionContext.Response = 
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errMessage);
            }
        }
    }
}