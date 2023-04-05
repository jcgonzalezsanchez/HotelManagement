using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    public class EmergencyContact : BaseModel
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [ForeignKey("Reservation")]
        public Guid ReservationId { get; set; }
        [JsonIgnore]
        public Reservation? Reservation { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long Phone { get; set; }
    }
}
