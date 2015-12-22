using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;
using Newtonsoft.Json;
using com.ndustrialio.api.ngest.serialize;


namespace com.ndustrialio.api.ngest
{
    public class TimeSeriesData
    {

        private String _feedKey;

        public static String TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static int MAX_MESSAGE_FIELDS = 50;
        public static int MIN_MESSAGE_FIELDS = 20;
        public static String TYPE = "timeseries";

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
                DateTime delocalized = TimeZoneInfo.ConvertTimeToUtc(entry.Key, fromTimeZone);

                // Store according to utc timestamp
                newData[delocalized] = entry.Value;    
            }

            _data = newData;
        }


        public List<String> getJSONData()
        {


            List<NgestMessage> nGestMessages = new List<NgestMessage>();

            List<TimeSeriesDataObject> dataBucket = 
                new List<TimeSeriesDataObject>();

            List<String> ret = new List<String>();


            // Organize the data into buckets of MAX_BUCKET_SIZE or less
            foreach (KeyValuePair<DateTime, TimeSeriesDataObject> entry in _data)
            {
                Dictionary<String, String> bucket = new Dictionary<String, String>();

                // Get the data at this timestamp
                Dictionary<String, String> fields = entry.Value.Data;

                // Now have appropriately sized bucket of MAX_BUCKET_SIZE or less
                NgestMessage message =
                    new NgestMessage(TimeSeriesData.TYPE,
                                    _feedKey,
                                    dataBucket);

                foreach (KeyValuePair<String, String> field in fields)
                {
                    bucket.Add(field.Key, field.Value);

                    if (bucket.Keys.Count == MAX_BUCKET_SIZE)
                    {

                    }
                }




                // We want to portion the buckets into 

                    // Accumulate bucket size
                    // This is basically the number of keys 
                    bucketSize += entry.Value.Length;

                // Determine if this data bucket is finished
                if ((bucketSize >= MAX_BUCKET_SIZE) && (dataBucket.Count > 0))
                {
                    // Data bucket full, add to list of buckets and clear out 
                    // for next time


                    nGestMessages.Add(message);
                    dataBucket = new List<TimestampedValues>();
                    bucketSize = 0;
                }

                // Something of a complicated data structure to serialize correctly
                TimestampedValues v =
                    new TimestampedValues(
                        entry.Key.ToString(TimeSeriesData.TIMESTAMP_FORMAT),
                        entry.Value.Data);

                dataBucket.Add(v);
            }

            if (dataBucket.Count > 0)
            {
                // Append any remainder values
                NgestMessage message =
                    new NgestMessage(TimeSeriesData.TYPE,
                                    _feedKey,
                                    dataBucket);

                nGestMessages.Add(message);
            }

            // Could do this in initial loop.. breaking it out to its own loop for simplicity
            foreach(var message in nGestMessages)
            {

                // Serialize and add to return list
                ret.Add(JsonConvert.SerializeObject(message, Formatting.Indented));
            }

            return ret;

        }

    }
}
