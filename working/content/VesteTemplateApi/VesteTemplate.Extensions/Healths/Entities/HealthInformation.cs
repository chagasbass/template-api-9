using VesteTemplate.Extensions.Health.Entities;

namespace VesteTemplate.Extensions.Monitoramentos.Entidades
{
    public class HealthInformation
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public List<HealthData> HealthDatas { get; set; }
        public MemoryInformation MemoryInformation { get; set; }

        public HealthInformation()
        {
            HealthDatas = new List<HealthData>();
        }
    }
}
