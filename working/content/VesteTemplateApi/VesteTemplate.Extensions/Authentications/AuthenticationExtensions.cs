namespace VesteTemplate.Extensions.Authentications;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var hasAuthentication = bool.Parse(configuration["BaseConfiguration:TemAutenticacao"]);

        if (hasAuthentication)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = "https://identitytoolkit.googleapis.com/google.identity.identitytoolkit.v1.IdentityToolkit";
                    options.Authority = "https://securetoken.google.com/liveretail-5366e";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/liveretail-5366e",
                        ValidateAudience = true,
                        ValidAudience = "liveretail-5366e",
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization();
        }

        return services;
    }

    public static WebApplication UseAuthenticationAndAuthorizationMiddlewares(this WebApplication app, IConfiguration configuration)
    {
        var hasAuthentication = bool.Parse(configuration["BaseConfiguration:TemAutenticacao"]);

        if (hasAuthentication)
        {
            app.UseRouting()
               .UseMiddleware<UnauthorizedTokenMiddleware>()
               .UseAuthentication()
               .UseAuthorization();
        }
        else
        {
            app.UseRouting();
        }

        return app;
    }
}


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ConditionalAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Obtém a configuração
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var hasAuthentication = bool.Parse(configuration["BaseConfiguration:TemAutenticacao"]);

        // Se a autenticação estiver desabilitada, permite o acesso
        if (!hasAuthentication)
            return;

        // Se estiver habilitada, verifica se o usuário está autenticado
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
