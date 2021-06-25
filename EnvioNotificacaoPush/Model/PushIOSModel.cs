using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvioNotificacaoPush.Model
{
    public class PushIOSModel
    {
        public string TeamId { get; set; }
        public string AppPackage { get; set; }
        public string POitoKeyId { get; set; }
        public string ChaveArquivoPOito { get; set; }
        public string PayloadPush { get; set; }
        public string DeviceToken { get; set; }
    }
}
