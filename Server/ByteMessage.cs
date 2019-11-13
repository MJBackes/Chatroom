using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ByteMessage
    {
        public byte[] Message { get; set; }
        public ByteMessage(int size = 256)
        {
            Message = new byte[size];
        }
    }
}
