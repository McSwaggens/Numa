using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numa
{
    public class NetworkInterfaceStatus
    {
        //Sent
        
        public long SentBytes;
        public long SentKiloBytes => SentBytes / 1024;
        public long SentMegaBytes => SentKiloBytes / 1024;
        public long SentGigaBytes => SentMegaBytes / 1024;

        public long SentBits => SentBytes / 8;
        public long SentKiloBits => SentKiloBytes / 8;
        public long SentMegaBits => SentMegaBytes / 8;
        public long SentGigaBits => SentGigaBytes / 8;

        //Received

        public long ReceivedBytes;
        public long ReceivedKiloBytes => ReceivedBytes / 1024;
        public long ReceivedMegaBytes => ReceivedKiloBytes / 1024;
        public long ReceivedGigaBytes => ReceivedMegaBytes / 1024;

        public long ReceivedBits => ReceivedBytes / 8;
        public long ReceivedKiloBits => ReceivedKiloBytes / 8;
        public long ReceivedMegaBits => ReceivedMegaBytes / 8;
        public long ReceivedGigaBits => ReceivedGigaBytes / 8;


        
    }
}
