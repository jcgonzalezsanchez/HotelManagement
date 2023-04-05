using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelManagement.Contracts.Models
{
    public class RoomType : BaseModel

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
        public string NameTypeRoom { get; set; }
    }
}
