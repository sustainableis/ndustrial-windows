using System;
using System.Collections.Generic;
using com.ndustrialio.api.utils;


namespace com.ndustrialio.api.ngest
{
    class TimeSeriesData
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

            // Organize the data into buckets of MAX_BUCKET_SIZE or less
            foreach (KeyValuePair<DateTime, TimeSeriesDataObject> entry in _data)
            {
                if (dataBucket.)
            }

    }
}
