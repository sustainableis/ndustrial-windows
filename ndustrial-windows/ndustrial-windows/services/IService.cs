
using System;
using System.Collections.Generic;

namespace com.ndustrialio.api.services
{
    public interface IService
    {
        ApiData get(Dictionary<String, String> args);

    }

}