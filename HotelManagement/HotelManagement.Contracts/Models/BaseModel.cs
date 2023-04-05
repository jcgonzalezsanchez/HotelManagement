using System.Runtime.Serialization;

namespace HotelManagement.Contracts.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public DateTime CreatedAt { get; set; }

        public BaseModel()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
