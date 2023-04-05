using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    public class Reservation : BaseModel
    {
        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [ForeignKey("Room")]
        public Guid RoomId { get; set; }
        [JsonIgnore]
        public Room? Room { get; set; }

        [DataMember]
        public DateTime DateEntry { get; set; }

        [DataMember]
        public DateTime DateDeparture { get; set; }

        [DataMember]
        public int NumberPeople { get; set; }

        [DataMember]
        public decimal CostTotal { get; set; }

        [DataMember]
        public ICollection<Guest>? Guests { get; set; }

        [DataMember]
        public ICollection<EmergencyContact>? EmergencyContacts { get; set; }
    }
}
