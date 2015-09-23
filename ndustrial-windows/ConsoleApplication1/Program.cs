using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.ndustrialio.api.ngest;

namespace com.ndustrialio.test      
{
    class Program
    {
        public static String FEED_KEY = "ngest-test-feed";

        static void Main(string[] args)
        {
            // Instantiate ngestClient
            var ngestClient = new NgestClient(args[0], FEED_KEY);

        }
    }
}
