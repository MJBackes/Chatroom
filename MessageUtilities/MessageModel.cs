using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageUtilities
{
    [Serializable]
    public class MessageModel : IChatLog
    {
        public string Message { get; set; }
        public string UserList { get; set; }
    }
}
