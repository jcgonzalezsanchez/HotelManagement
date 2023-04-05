using HotelManagement.Contracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    public class Guest : BaseModel
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
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateBirth { get; set; }

        [DataMember]
        public string Genre { get; set; }

        [DataMember]
        public DocumentType DucumentType { get; set; }

        [DataMember]
        public int DucumentNumber { get; set; }

        [DataMember]
        public string? Email { get; set; }

        [DataMember]
        public long Phone { get; set; }
    }
}
