using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Entities;

namespace LSCore.Contracts.SettingsModels
{
    public class LSCoreQueryableOptions<TEntity>
        where TEntity : LSCoreEntity
    {
        public ILSCoreFilter<TEntity>? Filter { get; set; }
        public ILSCoreIncludes<TEntity>? Includes { get; set; }
    }
}
