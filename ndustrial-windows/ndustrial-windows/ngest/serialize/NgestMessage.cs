using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.ndustrialio.api.ngest.serialize
{
    // This little class helps a lot with serialization
    public class NgestMessage
    {
        public String type;
        public List<TimestampedValues> data;
        public String feedKey;

        public NgestMessage(String type, String feedKey,
            List<TimestampedValues> data)
        {
            this.data = data;
            this.feedKey = feedKey;
            this.type = type;
        }
    }
}
