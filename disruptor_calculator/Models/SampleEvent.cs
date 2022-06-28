using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disruptor_calculator.Models
{
    public class SampleEvent
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double? Value { get; set; }
    }
}
