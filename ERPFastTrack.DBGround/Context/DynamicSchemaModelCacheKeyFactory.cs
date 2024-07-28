using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ERPFastTrack.DBGround.Context
{
    public class DynamicSchemaModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime) => context is ERPFastTrackUIContext dynamicContext
        ? (context.GetType(), dynamicContext.UseIntProperty, designTime)
        : (object)context.GetType();
    }

}
