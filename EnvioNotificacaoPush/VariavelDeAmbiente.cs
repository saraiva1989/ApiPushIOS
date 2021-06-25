using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvioNotificacaoPush
{
    public class VariavelDeAmbiente
    {
#if DEBUG
        public static string URLAPIAPPLE = "https://api.sandbox.push.apple.com:443/3/device/{0}";
#else
        public static string URLAPIAPPLE = "https://api.push.apple.com:443/3/device/{0}";
#endif
    }
}
