namespace VesteTemplate.Api.Extensions;

public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Classe destinada a resolução de dependência da aplicação.
    /// Scoped => Tempo de vida do processo de requisição (Ideal para APis)
    /// Transient => Toda vez que uma referência  é encontrada,é feita a criação de uma nova instância (ideal para Worker services)
    /// Singleton => Apenas uma instância durante a vida da aplicação. Somente quando é destruída é instanciada novamente.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection SolveAppDependencyInjenctions(this IServiceCollection services)
    {
        return services;
    }
}