using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    [DataContract]
    public class Hotel : BaseModel
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [ForeignKey("Agency")]
        public Guid AgencyId { get; set; }

        [JsonIgnore]
        public Agency? Agency { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public long Phone { get; set; }

        [DataMember]
        public double AgencyCommission { get; set; }

        [DataMember]
        public bool IsInactive { get; set; }

        [DataMember]
        public ICollection<Room>? Rooms { get; set; }
    }
}
