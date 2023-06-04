using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using SampleProject.Application.Configuration.Validation;

namespace SampleProject.Infrastructure.Processing
{
    public class HttpModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => ctx.Resolve<IHttpClientFactory>().CreateClient()).As<HttpClient>();
        }
    }
}