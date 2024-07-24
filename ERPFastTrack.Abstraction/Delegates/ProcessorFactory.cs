using ERPFastTrack.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.Abstraction.Delegates
{
    public delegate IProcessor ProcessorFactory(string key);
}
