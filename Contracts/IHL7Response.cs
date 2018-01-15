using System;

namespace Contracts
{
    public interface IHL7Response
    {
        Guid MessageId { get; set; }
        string Message { get; set; }
    }
}
