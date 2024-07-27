using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ERPFastTrack.DBGround.Context
{
    public class DynamicSchemaModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
        {
            if (context is ERPFastTrackUIContext dynamicContext)
            {
                return (context.GetType(), dynamicContext.Schema);
            }

            return context.GetType();
        }
    }

}
