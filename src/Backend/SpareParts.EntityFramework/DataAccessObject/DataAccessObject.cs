using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SpareParts.DataAccessObject;

namespace SpareParts.EntityFramework.DataAccessObject
{
    public abstract class DataAccessObject<TDataModel, TReadModel, TDbContext> : IDataAccessObject<TReadModel>, IAsyncEnumerable<TReadModel>
        where TReadModel : class
        where TDbContext : DbContext
    {
        protected DataAccessObject(TDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDbContext Context { get; set; }

        protected abstract IQueryable<TReadModel> Query { get; }

        public IEnumerator<TReadModel> GetEnumerator() => Query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Type ElementType => Query.ElementType;

        public Expression Expression => Query.Expression;

        public IQueryProvider Provider => Query.Provider;

        IAsyncEnumerator<TReadModel> IAsyncEnumerable<TReadModel>.GetEnumerator() => ((IAsyncEnumerable<TReadModel>)Query).GetEnumerator();
    }
}
