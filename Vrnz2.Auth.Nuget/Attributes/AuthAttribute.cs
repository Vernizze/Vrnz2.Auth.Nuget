using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Vrnz2.Auth.Nuget.DTOs;
using Vrnz2.Auth.Nuget.Handler;
using Vrnz2.Infra.CrossCutting.Libraries.HttpClient;

namespace Vrnz2.Auth.Nuget.Attributes
{
    public class AuthAttribute
        : TypeFilterAttribute
    {
        #region Constructors

        public AuthAttribute(params string[] values)
            : base(typeof(AuthFilter))
        {
            var claims = new List<Claim>();

            if (values != null && values.Length > 0)
                for (int i = 0; i < values.Length; i++)
                    claims.Add(new Claim(ClaimTypes.Role, values[i]));

            Arguments = new object[] { claims };
        }

        #endregion
    }

    public class AuthFilter 
        : IAuthorizationFilter
    {
        #region Variables

        private readonly List<Claim> _claims;

        #endregion

        #region Constructors

        public AuthFilter(List<Claim> claims)
            => _claims = claims;

        #endregion

        #region Methods

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["token"];

                var claims = context.HttpContext.User.Claims.Select(s => s.Value).ToList();

                var response = HttpRequestFactory.Post(SettingsHandler.Instance.AuthSettings.AutServerUrl, new AuthServerRequest { token = token.ToString(), claims = claims }).Result;

                context.Result = GetResult(response);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Authentication Error! - Error: {ex.Message}-{ex.StackTrace}");

                context.Result = new ForbidResult();
            }
        }

        private ActionResult GetResult(HttpResponseMessage response) 
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Accepted:
                    return new AcceptedResult();
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();
                case HttpStatusCode.Forbidden:
                    return new ForbidResult();
                default:
                    return new UnauthorizedResult();
            }
        }

        #endregion
    }
}

//var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
//if (!hasClaim)
//{
//    context.Result = new ForbidResult();
//}
