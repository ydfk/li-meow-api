using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Linq.Expressions;
using LiMeowApi.Entity.Base;
using LiMeowApi.Schema.Base;

namespace LiMeowApi.Repository
{
    public interface IRepository<TEntity>
    {
        IMongoQueryable<TEntity> QueryData();

        IMongoCollection<TEntity> GetCollection();

        Task<TModel> Get<TModel>(Expression<Func<TEntity, bool>> filter);

        Task<List<TModel>> List<TModel>(Expression<Func<TEntity, bool>> filter);

        Task<List<TModel>> List<TModel>();

        Task<List<TModel>> ListByIds<TModel>(List<string> ids);

        PageResultModel<TModel> PageData<TModel>(Expression<Func<TEntity, bool>> filter, PageQueryModel pageQuery);

        Task<long> Count(Expression<Func<TEntity, bool>> filter);

        Task<TModel> Save<TModel>(TModel model);

        Task<TModel> Update<TModel>(TModel model);

        Task<long> Delete(Expression<Func<TEntity, bool>> filter);
    }
}