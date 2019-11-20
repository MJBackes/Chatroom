using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageUtilities;
namespace Server
{
    public interface IServer
    {
        void Join(IClient client);
        void Leave(IClient client);
        void Notify(IChatLog message);

    }
}
