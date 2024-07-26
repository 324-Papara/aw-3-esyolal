using System.Text.Json.Serialization;
using Para.Base.Schema;

namespace Para.Schema;

public class CustomerPhoneRequest : BaseRequest
{
    [JsonIgnore]
    public long CustomerId { get; set; }
    public string CountryCode { get; set; }
    public string Phone { get; set; }
    public bool IsDefault { get; set; }
}


public class CustomerPhoneResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string CountryCode { get; set; }
    public string Phone { get; set; }
    public bool IsDefault { get; set; }
}
