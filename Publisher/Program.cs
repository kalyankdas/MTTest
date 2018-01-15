using Common;
using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var busSettings = BusSettings.LoadBusSettings();
            var busControlFactory = new BusControlFactory();

            var busControl = busControlFactory.CreateBus(busSettings);
            Console.Write("");

            busControl.Start();
            //event example
          /*  Task.Run(async ()=> {
                await busControl.Publish<IHl7MessageReceived>(new Hl7MessageReceived { DateUpdated = DateTime.Now, Message = "MSH1,1|EVENT" }).ConfigureAwait(false);
            });*/


            busControl.Publish<IHl7MessageReceived>(new Hl7MessageReceived
            {
                DateUpdated = DateTime.Now,
                Message = "MSH1,1|EVENT"
            }).Wait();

            // command example

            var sendEndpoint =  busControlFactory.GetSendEndpoint(busControl, "sampleQ").Result;
            Task.Run(async () =>
            {
                await sendEndpoint.Send<IHl7Message>(new Hl7Message { CreationDate = DateTime.Now, Message = "MSH1,1 | COMMAND" });

            });


            Console.Read();
            busControl.Stop();

        }
    }
}
