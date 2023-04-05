using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HotelManagement.Contracts.Models
{
    [DataContract]
    public class Agency : BaseModel
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataMember]
        public long Phone { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public ICollection<Hotel>? Hotels { get; set; }
    }
}
