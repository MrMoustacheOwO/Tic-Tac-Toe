using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Views
{
    public interface IClosable
    {
        event EventHandler<EventArgs> RequestClose;
    }
}
