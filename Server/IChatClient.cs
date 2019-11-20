using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageUtilities;
namespace Server
{
    public interface IChatClient
    {
        Task<IChatLog> Recieve();
    }
}
