
using System;
using System.Collections.Generic;

namespace com.ndustrialio.api.services
{
    public interface IService
    {
        Dictionary<string, string> get(Dictionary<String, String> args);

    }

}