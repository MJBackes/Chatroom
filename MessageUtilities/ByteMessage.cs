﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtilities
{
    public class ByteMessage
    {
        public byte[] Message { get; set; }
        public ByteMessage(int size = 2560)
        {
            Message = new byte[size];
        }
    }
}
