using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Spy.background
{
    class Control
    {
    static public void shutdown()
    {
        Process.Start("shutdown.exe","-s -t 5");
    }
    static public void lockin()
    {
        Process.Start("shutdown.exe", "-l");
    }
    }
}
