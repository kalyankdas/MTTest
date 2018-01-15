using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class Hl7MessageReceivedConsumer : IConsumer<IHl7MessageReceived>
    {
        public async Task Consume(ConsumeContext<IHl7MessageReceived> context)
        {
            await Console.Out.WriteLineAsync($"Consumer  -> Date updated: " +
                $"{context.Message.DateUpdated}, Message: {context.Message.Message}").ConfigureAwait(false);
        }
    }
}
