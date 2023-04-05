using System.Runtime.Serialization;

namespace HotelManagement.Contracts.Enums
{
    [DataContract]
    public enum DocumentType
    {
        [EnumMember(Value = "CC")]
        CC = 1,

        [EnumMember(Value = "CE")]
        CE = 2,

        [EnumMember(Value = "NIP")]
        NIP = 3,

        [EnumMember(Value = "NIT")]
        NIT = 4,

        [EnumMember(Value = "TI")]
        TI = 5,

        [EnumMember(Value = "PAP")]
        PAP = 6,
    }
}
