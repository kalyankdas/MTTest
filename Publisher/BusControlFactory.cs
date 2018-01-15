using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Logging;
using Common;

namespace Publisher
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
                sbc.Durable = true;
                sbc.AutoDelete = true;

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
            var uriWithVHost = RemoveLastSegment(busControl.Address);
            var address = new Uri(uriWithVHost.OriginalString + "/"+ qName);
            return await GetSendEndpoint(busControl, address);
        }

        private Uri RemoveLastSegment(Uri uri)
        {
            var newSegments = uri.Segments.Take(uri.Segments.Length - 1).ToArray();
            newSegments[newSegments.Length - 1] =
                newSegments[newSegments.Length - 1].TrimEnd('/');
            var ub = new UriBuilder(uri.GetLeftPart(UriPartial.Path)) {Path = string.Concat(newSegments)};
            //ub.Query=string.Empty;  //maybe?
            var newUri = ub.Uri;
            return newUri;
        }

    }
}
