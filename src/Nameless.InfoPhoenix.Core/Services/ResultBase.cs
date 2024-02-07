using System.Diagnostics.CodeAnalysis;
using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services
{
    public abstract class ResultBase<TEntity> where TEntity : EntityBase
    {
        #region Public Properties

        public TEntity? Entity { get; set; }

        public string? Error { get; set; }

        [MemberNotNullWhen(returnValue: true, nameof(Entity))]
        public bool Succeeded => Error is null;

        #endregion
    }
}
