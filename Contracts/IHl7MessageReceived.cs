using System;

namespace Contracts
{
    public interface IHl7MessageReceived
    {
        DateTime DateUpdated { get; set; }
        string Message { get; set; }
    }

    public class Hl7MessageReceived : IHl7MessageReceived
    {
        public DateTime DateUpdated { get; set; }
        public string Message { get; set; }
       
    }
}
