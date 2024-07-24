using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.Abstraction.Interfaces
{
    public interface IProcessor
    {
        Task<T> RunAsync<T, R>(R request) where T : new();
    }
}
