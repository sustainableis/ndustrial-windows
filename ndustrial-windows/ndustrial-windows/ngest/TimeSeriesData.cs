using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;
using Newtonsoft.Json;


namespace com.ndustrialio.api.ngest
{
    public class TimeSeriesData
    {
        private String _feedKey;

        public static String TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static int MAX_BUCKET_SIZE = 50;

        private SortedDictionary<DateTime, TimeSeriesDataObject> _data;

        public TimeSeriesData(String feed_key)
        {
            _feedKey = feed_key;

            _data = new SortedDictionary<DateTime, TimeSeriesDataObject>();
        }


        public void addValue(DateTime timestamp, String field, Object value)
        {
            if (!_data.ContainsKey(timestamp))
            {
                // No value for this timestamp
                var obj = new TimeSeriesDataObject();

                obj.addValue(field, value);

                _data[timestamp] = obj;
            } else
            {
                // Existing value for this timestamp
                var obj = _data[timestamp];

                // Watch for the field already existing
                // at this point in time
                try
                {
                    obj.addValue(field, value);
                } catch (NonUniqueFieldIDException e)
                {
                    Console.WriteLine("Field ID: " + field + " already exists at: "
                        + timestamp.ToString(TIMESTAMP_FORMAT));
                }
            }
        }


        public void delocalizeTimestamps(String localTimeZone)
        {
            SortedDictionary<DateTime, TimeSeriesDataObject> newData =
                new SortedDictionary<DateTime, TimeSeriesDataObject>();

            // Look up timezone in system
            TimeZoneInfo fromTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZone);
            
            foreach(KeyValuePair<DateTime, TimeSeriesDataObject> entry in _data)
            {
                // Convert timestamp to utc
                DateTime delocalized = TimeZoneInfo.ConvertTime(entry.Key, fromTimeZone, TimeZoneInfo.Utc);

                // Store according to utc timestamp
                newData[delocalized] = entry.Value;    
            }

            _data = newData;
        }


        public List<String> getJSONData()
        {
            // Whoa
            List<List<Dictionary<String, String>>> dataBuckets = new List<List<Dictionary<String, String>>>();

            List<Dictionary<String, String>> dataBucket = new List<Dictionary<String, String>>();

            List<String> ret = new List<String>();

            int bucketSize = 0;

            // Organize the data into buckets of MAX_BUCKET_SIZE or less
            foreach (KeyValuePair<DateTime, TimeSeriesDataObject> entry in _data)
            {
                // Accumulate bucket size
                bucketSize += entry.Value.Length;

                // Determine if this data bucket is finished
                if ((bucketSize >= MAX_BUCKET_SIZE) && (dataBucket.Count > 0))
                {
                    // Data bucket full, add to list of buckets and clear out 
                    // for next time
                    dataBuckets.Add(dataBucket);
                    dataBucket = new List<Dictionary<String, String>>();
                    bucketSize = 0;
                }

                // Add new value into databucket
                dataBucket.Add(new Dictionary<String, String>
                    { {"timestamp: ", entry.Key.ToString(TIMESTAMP_FORMAT) },
                      {"data", entry.Value.toJSON()}
                    });
            }

            if (dataBucket.Count > 0)
            {
                // Append any remainder values
                dataBuckets.Add(dataBucket);
            }

            // Could do this in initial loop.. breaking it out to its own loop for simplicity
            foreach(var bucket in dataBuckets)
            {
                Dictionary<String, String> retData = new Dictionary<String, String>();

                // Construct JSONObject to be sent
                retData["feedKey"] = _feedKey;
                retData["type"] = "timeseries";
                retData["data"] = JsonConvert.SerializeObject(bucket, Formatting.None);

                // Serialize and add to return list
                ret.Add(JsonConvert.SerializeObject(retData, Formatting.None));
            }

            return ret;

        }

    }
}
