using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.ndustrialio.api.ngest.serialize
{
    // This little class makes serialization much easier

    //{timestamp: xxxxxxxx, data: {field1: {value: value1}, field2: {value: value2}.....

    public class TimestampedValues
    {
        public String timestamp;
        public Dictionary<String, Dictionary<String, String>> data;

        public TimestampedValues(String timestamp,
            Dictionary<String, Dictionary<String, String>> data)
        {
            this.timestamp = timestamp;
            this.data = data;
        }
    }
}
