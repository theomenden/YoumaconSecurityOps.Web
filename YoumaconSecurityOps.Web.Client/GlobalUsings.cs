﻿global using System.IO;
global using System.Linq;
global using System.Net.Mime;
global using Blazorise;
global using Blazorise.Bootstrap5;
global using Blazorise.Icons.FontAwesome;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Components.Server.Circuits;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.ResponseCompression;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using YoumaconSecurityOps.Core.AutoMapper.Extensions;
global using YoumaconSecurityOps.Core.EventStore.Extensions;
global using YoumaconSecurityOps.Core.FluentEmailer.Extensions;
global using YoumaconSecurityOps.Core.Mediatr.Extensions;
global using YoumaconSecurityOps.Core.Shared.Configuration;
global using YoumaconSecurityOps.Data.EntityFramework.Extensions;
global using YoumaconSecurityOps.Web.Client.Extensions;
global using YoumaconSecurityOps.Web.Client.Middleware;
global using YoumaconSecurityOps.Web.Client.Models;
global using YsecItAuthApp.Web.EntityFramework.Extensions;
global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Blazorise.DataGrid;
global using Microsoft.AspNetCore.Components;
global using YoumaconSecurityOps.Core.Mediatr.Commands;
global using YoumaconSecurityOps.Core.Mediatr.Queries;
global using YoumaconSecurityOps.Core.Shared.Enumerations;
global using YoumaconSecurityOps.Core.Shared.Models.Readers;
global using YoumaconSecurityOps.Web.Client.Services;