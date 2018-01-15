using Common;
using MassTransit;
using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var busSettings = BusSettings.LoadBusSettings();
            var busControlFactory = new BusControlFactory();

            var busControl = busControlFactory.CreateBus(busSettings);

            busControl.Start();
            Console.Read();
            busControl.Stop();
        }
    }
}
