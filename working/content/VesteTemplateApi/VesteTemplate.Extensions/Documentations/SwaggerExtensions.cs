namespace VesteTemplate.Extensions.Documentations;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        var hasAuthentication = bool.Parse(configuration["BaseConfiguration:TemAutenticacao"]);

        #region Criar versões diferentes de rotas
        services.AddSwaggerGen(c =>
        {
            #region aplicando o filtro de parâmetros não obrigatórios nas rotas no swagger
            c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
            #endregion

            #region Resolver conflitos de rotas
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            #endregion

            #region Add comentários aos endpoints
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
            #endregion

            #region Inserindo Autenticação Bearer no swagger

            if (hasAuthentication)
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Autorização efetuada via JWT token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);
            }

            #endregion

            #region Agrupamento de controllers

            c.TagActionsBy(api =>
            {
                if (api.GroupName != null)
                {
                    return new[] { api.GroupName };
                }

                if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    return new[] { controllerActionDescriptor.ControllerName };
                }

                throw new InvalidOperationException("Unable to determine tag for endpoint.");
            });

            c.DocInclusionPredicate((docName, apiDesc) =>
            {
                var apiVersionMetadata = apiDesc.ActionDescriptor.EndpointMetadata
                                            .OfType<ApiVersionAttribute>()
                                            .SelectMany(attr => attr.Versions)
                                            .Select(v => $"v{v.MajorVersion}{(v.MinorVersion > 0 ? "." + v.MinorVersion : "")}")
                                            .ToList();

                if (!apiVersionMetadata.Any())
                {
                    return docName == "v1";
                }

                return apiVersionMetadata.Any(version => version == docName);
            });

            #endregion
        });

        #endregion

        services.ConfigureOptions<SwaggerOptionsExtensions>();

        return services;
    }

    /// <summary>
    /// Extensão para configurar rotas iguais mas com versões diferentes
    /// </summary>
    /// <param name="app"></param>
    /// <param name="provider"></param>
    public static void UseSwaggerUIMultipleVersions(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }
}
