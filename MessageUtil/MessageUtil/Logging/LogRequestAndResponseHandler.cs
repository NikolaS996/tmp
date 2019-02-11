using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageUtil.Logging
{
    //This class will be used to intercept every request and response and log in into the file
    //With singleton instance of FileLogger
    //In order to use this intercepter you must include it in WebApiConfig.cs of the Microservice
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // log request body
            string requestBody = await request.Content.ReadAsStringAsync();
            // using created FileLogger Singleton
            LogHelper.Log(LogTarget.File, String.Format("Request-  \n\tHeaders: {0}\n\tBody: {1}", request.Headers.ToString(), requestBody), DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                LogHelper.Log(LogTarget.File, String.Format("Response-  \tStatus:{0}\n\tBody: {1}", result.StatusCode, responseBody), DataFormatUtil.GetFormatedLongDateTimeString(DateTime.Now));
            }

            return result;
        }
    }
}
