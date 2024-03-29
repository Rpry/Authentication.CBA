﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Demo.Authentication.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if (user != null)
            {
                var isAuthenticated = user.Identity.IsAuthenticated;
                var userClaims = user.Claims;    
            }
            
            await _next(context);
        }
    }

    public static class Extensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
      
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}