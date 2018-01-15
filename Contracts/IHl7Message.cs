using System;

namespace Contracts
{
    public interface IHl7Message
    {
        DateTime CreationDate { get; set; }
        string Message { get; set; }
    }

    public class Hl7Message : IHl7Message
    {
        public DateTime CreationDate { get; set; }
        public string Message { get; set; }
    }
}
