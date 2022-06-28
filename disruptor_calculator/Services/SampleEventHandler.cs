using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disruptor;
using disruptor_calculator.Models;

namespace disruptor_calculator.Services
{
    public class SampleEventHandler : IEventHandler<SampleEvent>
    {
        public void OnEvent(SampleEvent data, long sequence, bool endOfBatch)
        {
            Console.WriteLine($"Operation id: {data.Id}, Code: {data.Code}, Result: {data.Value}");
        }
    }
}
