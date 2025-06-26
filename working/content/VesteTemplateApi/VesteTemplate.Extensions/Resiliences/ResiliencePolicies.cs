namespace VesteTemplate.Extensions.Resiliences;

public static class ResiliencePolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetApiRetryPolicy(int quantidadeDeRetentativas)
    {
        var quantidadeTotalDeRetentativas = quantidadeDeRetentativas;

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode != HttpStatusCode.OK)
            .RetryAsync(quantidadeDeRetentativas, onRetry: (message, numeroDeRetentativas) =>
          {
              if (quantidadeTotalDeRetentativas == numeroDeRetentativas)
              {
                  //TODO: Aqui é só pegar os dados da requisição
              }
          });
    }
}
