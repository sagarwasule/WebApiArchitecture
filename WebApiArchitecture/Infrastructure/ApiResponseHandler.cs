using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApiArchitecture.Infrastructure
{
    public class ApiResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            string errorMessage = null;

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;

                    foreach (var err in error)
                    {
                        errorMessage += err.Key + " : " + err.Value;
                    }
                }
            }

            var newResponse = request.CreateResponse(response.StatusCode, new ApiResult(response.StatusCode, content, errorMessage));

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private void LogRequest(HttpRequestMessage request)
        {
            (request.Content ?? new StringContent("")).ReadAsStringAsync().ContinueWith(x =>
            {
                Logger logger = LogManager.GetLogger("databaselogger");
                logger.Info("Request : {4:yyyy-MM-dd HH:mm:ss} Token : {5} CorrelationId : {0} request [{1}]{2} - {3}", request.GetCorrelationId(), request.Method, request.RequestUri, x.Result, DateTime.Now, encryptedToken);
            });
        }

        private void LogResponse(HttpResponseMessage response)
        {
            var request = response.RequestMessage;
            (response.Content ?? new StringContent("")).ReadAsStringAsync().ContinueWith(x =>
            {
                Logger logger = LogManager.GetLogger("databaselogger");
                logger.Info("Response : {3:yyyy-MM-dd HH:mm:ss} {4} {0} response [{1}] - {2}", request.GetCorrelationId(), response.StatusCode, x.Result, DateTime.Now, "");
            });
        }

    }
}