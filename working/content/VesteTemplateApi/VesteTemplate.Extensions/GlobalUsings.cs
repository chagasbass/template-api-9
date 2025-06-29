﻿global using HealthChecks.UI.Client;
global using Microsoft.ApplicationInsights.Extensibility;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.RateLimiting;
global using Microsoft.AspNetCore.ResponseCompression;
global using Microsoft.Data.SqlClient;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Polly;
global using Polly.Extensions.Http;
global using Serilog;
global using Serilog.Core;
global using Serilog.Events;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using System.IO.Compression;
global using System.Net;
global using System.Net.Mail;
global using System.Net.Mime;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Threading.RateLimiting;
global using VesteTemplate.Extensions.Documentations.Filters;
global using VesteTemplate.Extensions.Health.Customs;
global using VesteTemplate.Extensions.Health.Entities;
global using VesteTemplate.Extensions.Healths;
global using VesteTemplate.Extensions.Logs.Configurations;
global using VesteTemplate.Extensions.Logs.Entities;
global using VesteTemplate.Extensions.Logs.Services;
global using VesteTemplate.Extensions.Middlewares;
global using VesteTemplate.Extensions.Monitoramentos.Entidades;
global using VesteTemplate.Extensions.Notifications;
global using VesteTemplate.Factories;
global using VesteTemplate.Shared.Configurations;
global using VesteTemplate.Shared.Entities;
global using VesteTemplate.Shared.Enums;
global using VesteTemplate.Shared.Extensions;
global using VesteTemplate.Shared.Helpers;
global using VesteTemplate.Shared.Notifications;
