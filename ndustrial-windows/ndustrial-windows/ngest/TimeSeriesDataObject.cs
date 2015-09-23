using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.ndustrialio.api.utils;
using Newtonsoft.Json;

namespace com.ndustrialio.api.ngest
{
    class TimeSeriesDataObject
    {
        private Dictionary<String, Object> _data;

        public TimeSeriesDataObject()
        {
            _data = new Dictionary<String, Object>();
        }


        public void addValue(String field, Object value)
        {
            if (_data.ContainsKey(field))
            {
                throw new NonUniqueFieldIDException();
            } else
            {
                _data.Add(field, value);
            }
        }

        public String toJSON()
        {
            return JsonConvert.SerializeObject(_data, Formatting.None);
        }

    }
}
