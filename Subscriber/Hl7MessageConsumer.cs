using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class Hl7MessageConsumer : IConsumer<IHl7Message>
    {
        public async Task Consume(ConsumeContext<IHl7Message> context)
        {
            await Console.Out.WriteLineAsync($"Hl7 message consumer  -> Date crated: " +
                $"{context.Message.CreationDate}, Message: {context.Message.Message}").ConfigureAwait(false);
        }
    }
}
