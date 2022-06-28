using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using Disruptor.Dsl;
using disruptor_calculator.Models;
using disruptor_calculator.Services;

namespace disruptor_calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var disruptor = new Disruptor<SampleEvent>(() => new SampleEvent(), ringBufferSize: 1024);
            string calc1 = "1,Sum,5,3";
            string calc2 = "2,Diff,5,3";
            string calc3 = "3,Mult,5,3";
            disruptor.HandleEventsWith(new SampleEventHandler());
            disruptor.Start();
            var ringBuffer = disruptor.RingBuffer;

            var producer = new SampleEventProducer(ringBuffer);

            producer.ProduceUsingRawApi(calc1);
            Thread.Sleep(1000);
            producer.ProduceUsingRawApi(calc2);
            Thread.Sleep(1000);
            producer.ProduceUsingRawApi(calc3);

        }
    }
}
