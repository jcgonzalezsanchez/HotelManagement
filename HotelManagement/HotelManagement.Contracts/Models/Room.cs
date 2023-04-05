using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    public class Room : BaseModel
    {

        [Key]
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        [ForeignKey("Hotel")]
        public Guid HotelId { get; set; }
        [JsonIgnore]
        public Hotel? Hotel { get; set; }

        [DataMember]
        public RoomType? RoomType { get; set; }

        [DataMember]
        public Location? Location { get; set; }

        [DataMember]
        public int RoomNumber { get; set; }

        [DataMember]
        public decimal Basecost { get; set; }

        [DataMember]
        public decimal Tax { get; set; }

        [DataMember]
        public bool IsInactive { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
