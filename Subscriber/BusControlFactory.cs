using MassTransit;
using System;
using System.Threading.Tasks;
using Common;

namespace Subscriber
{
    public class BusControlFactory
    {
        public IBusControl CreateBus(BusSettings busSettings)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri(busSettings.Uri), h =>
                {
                    h.Username(busSettings.Username);
                    h.Password(busSettings.Password);
                    h.Heartbeat(busSettings.HeartBeatInSeconds);
                });

                sbc.ReceiveEndpoint(host, busSettings.IncomingQueue, cfg => {
                    cfg.Consumer<Hl7MessageConsumer>();
                    cfg.Consumer<Hl7MessageReceivedConsumer>();
                });
                sbc.Durable = true;
                //sbc.AutoDelete = false;
               
                //sbc.UseNLog();
            });
            
            return bus;
        }

        public async Task<ISendEndpoint> GetSendEndpoint(IBusControl busControl, Uri address)
        {
            return await busControl.GetSendEndpoint(address);
        }
        public async Task<ISendEndpoint> GetSendEndpoint(IBusControl busControl, string qName)
        {
            var address = new Uri(busControl.Address, new Uri(qName));
            return await GetSendEndpoint(busControl, address);
        }
       
    }
}
