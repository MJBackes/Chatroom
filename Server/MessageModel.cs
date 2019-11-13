using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class MessageModel : IChatLog
    {
        public string Message { get; set; }
        public string UserList { get; set; }
    }
}
