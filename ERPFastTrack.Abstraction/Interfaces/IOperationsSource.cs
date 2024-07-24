using ERPFastTrack.Abstraction.Models.SourceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.Abstraction.Interfaces
{
    public interface IOperationsSource<T>
    {
        SourceDataDetails GetDetails(T Req); 
    }
}
