using System;
using System.Collections.Generic;
using System.Text;

namespace Bawbee.Mobile.Configs
{
    public static class AppConfiguration
    {
        public static string BASE_URL = App.IsAndroid ? "http://10.0.2.2:5000" : "http://localhost:5000";
    }
}
