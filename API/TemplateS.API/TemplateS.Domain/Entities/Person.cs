using TemplateS.Domain.Common;

namespace TemplateS.Domain.Entities
{
    public sealed class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
