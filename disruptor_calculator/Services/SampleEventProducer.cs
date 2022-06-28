using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using Disruptor;
using disruptor_calculator.Enums;
using disruptor_calculator.Models;

namespace disruptor_calculator.Services
{
    public class SampleEventProducer
    {
        private readonly RingBuffer<SampleEvent> _ringBuffer;

        public SampleEventProducer(RingBuffer<SampleEvent> ringBuffer)
        {
            _ringBuffer = ringBuffer;
        }

        public void ProduceUsingRawApi(string input)
        {
            // Grab the next sequence
            var sequence = _ringBuffer.Next();
            try
            {
                // Get the event in the Disruptor for the sequence
                var data = _ringBuffer[sequence];
                int? sum = 0;
                // Fill with data
                var str = input.Split(",");
                string code = "";
                switch (str[1])
                {
                    case "Sum":
                        sum = int.Parse(str[2]) + int.Parse(str[3]);
                        code = "Success";
                        break;
                    case "Diff":
                        sum = int.Parse(str[2]) - int.Parse(str[3]);
                        code = "Success";
                        break;
                    default:
                        code = "OperationNotFound";
                        sum = null;
                        break;
                }
                data.Id = int.Parse(str[0]);
                data.Code = code;
                data.Value = sum;
            }
            finally
            {
                // Publish the event
                _ringBuffer.Publish(sequence);
            }
        }

        //public void ProduceUsingScope(ReadOnlyMemory<byte> input)
        //{
        //    using (var scope = _ringBuffer.PublishEvent())
        //    {
        //        var data = scope.Event();

        //        // Fill with data
        //        data.Id = MemoryMarshal.Read<int>(input.Span);
        //        data.Value = MemoryMarshal.Read<double>(input.Span.Slice(4));

        //        // The event is published at the end of the scope
        //    }
        //}
    }
}