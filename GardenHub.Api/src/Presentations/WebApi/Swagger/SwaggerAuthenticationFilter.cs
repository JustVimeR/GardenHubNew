using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebApi.Swagger;

public class SwaggerAuthenticationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        List<AuthorizeAttribute> authorizeAttributes = GetAttributes<AuthorizeAttribute>(
            context.MethodInfo, context.MethodInfo.DeclaringType);

        List<AllowAnonymousAttribute> allowAnonymousAttributes = GetAttributes<AllowAnonymousAttribute>(
            context.MethodInfo, context.MethodInfo.DeclaringType);


        if (allowAnonymousAttributes.Any())
        {
            operation.Security = null;
        }
        else if (authorizeAttributes.Any())
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        }, Array.Empty<string>()
                    }
                }
            };
        }
        else
        {
            operation.Security = null;
        }
    }

    private List<T> GetAttributes<T>(MethodInfo methodInfo, Type? declaringType) where T : Attribute
    {
        return methodInfo
            .GetCustomAttributes(true)
            .OfType<T>()
            .Concat(declaringType?.GetCustomAttributes(true).OfType<T>() ?? Enumerable.Empty<T>())
            .ToList();
    }
}