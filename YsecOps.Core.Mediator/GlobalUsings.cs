#region Dependency Frameworks Usings
global using LazyCache;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.Reflection;
global using System.Text.Json;
#endregion
#region Youma Usings
global using YSecOps.Data.EfCore.Models;
global using YSecOps.Events.EfCore.Models;
global using YsecOps.Core.Mediator.Infrastructure;
global using YsecOps.Core.Mediator.Requests.Commands;
global using YsecOps.Core.Mediator.Requests.Queries;
global using YsecOps.Core.Mediator.Requests.Queries.Streaming;
global using YsecOps.Core.Mediator.Pipelines.Behaviors;
global using YsecOps.Core.Mediator.Pipelines.Caching;
global using YsecOps.Core.Mediator.Pipelines.Processors;
global using YSecOps.Data.EfCore.Contexts;
global using YoumaconSecurityOps.Core.Shared.Parameters;
global using YoumaconSecurityOps.Core.Shared.Extensions;
#endregion