using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.Header;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FamilyHubs.ServiceDirectory.Admin.Web.ViewModel
{
    public class InputPageViewModel : HeaderPageModel, IHasErrorStatePageModel
    {
        [BindProperty]
        public string BackButtonPath { get; set; } = string.Empty;
        public string SubmitButtonText { get; set; } = "Continue";
        public IErrorState Errors { get; protected set; } = ErrorState.Empty;
        public string PageHeading { get; set; } = string.Empty;
        public string HintText { get; set; } = string.Empty;

        /// <summary>
        /// Sets the back button path using HttpContext
        /// </summary>
        protected void SetBackButtonPath()
        {
            if (!string.IsNullOrEmpty(BackButtonPath))
            {
                return;
            }

            var host = GetHost();
            var path = HttpContext.Request.Headers.Referer;

            if(StringValues.IsNullOrEmpty(path))
            {
                throw new InvalidOperationException("Request does not contain a path");
            }

            BackButtonPath = path.ToString()[(path.ToString().IndexOf(host) + host.Length)..];
        }

        private string GetHost()
        {
            var host = HttpContext.Request.Host.Value;

            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-Host"))
            {
                var xforwardedhost = HttpContext.Request.Headers["X-Forwarded-Host"];
                if (!StringValues.IsNullOrEmpty(xforwardedhost))
                {
                    host = xforwardedhost.ToString();
                }
            }

            return host;
            
        }

    }
}
