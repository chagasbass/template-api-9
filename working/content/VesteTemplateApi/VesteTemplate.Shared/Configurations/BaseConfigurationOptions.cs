namespace VesteTemplate.Shared.Configurations;

public class BaseConfigurationOptions
{
    public const string? BaseConfig = "BaseConfiguration";
    public string? NomeAplicacao { get; set; }
    public string? Desenvolvedor { get; set; }
    public string? Descricao { get; set; }
    public bool TemAutenticacao { get; set; }
    public string? StringConexaoBancoDeDados { get; set; }
    public string? StringConexaoBancoLinx { get; set; }
    public bool HabilitarMensagensDeLog { get; set; }

    public BaseConfigurationOptions() { }

}
