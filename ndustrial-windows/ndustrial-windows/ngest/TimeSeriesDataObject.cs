using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.ndustrialio.api.utils;
using Newtonsoft.Json;

namespace com.ndustrialio.api.ngest
{

    public class TimeSeriesDataObject
    {
        private Dictionary<String, String> _data;

        public TimeSeriesDataObject()
        {
            _data = new Dictionary<String, String>();
        }


        public void addValue(String field, Object value)
        {
            if (_data.ContainsKey(field))
            {
                throw new NonUniqueFieldIDException();
            }
            else
            {
                _data.Add(field, value.ToString());
            }
        }

        public Dictionary<String, String> Data
        {
            get { return _data; }
        }

        public int Length
        {
            get { return _data.Keys.Count; }
        }

    }
}
