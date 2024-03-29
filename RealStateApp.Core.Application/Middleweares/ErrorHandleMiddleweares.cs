using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Middleweares
{
    public class ErrorHandleMiddlewear
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddlewear(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Suceded = false, Message = error?.Message };

                switch (error)
                {
                    case ApiEception e:
                        switch (e.ErrorCode)
                        {
                            case (int)HttpStatusCode.BadRequest:
                                response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;

                            case (int)HttpStatusCode.NotFound:
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;

                            case (int)HttpStatusCode.NoContent:
                                response.StatusCode = (int)HttpStatusCode.NoContent;
                                break;

                            default:
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                        break;

                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
