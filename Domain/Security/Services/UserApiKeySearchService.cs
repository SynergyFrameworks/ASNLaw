using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Search;
using Scion.Business.Security.Repositories;
using Scion.Infrastructure.Extensions;

namespace Scion.Business.Security.Services
{
    public class UserApiKeySearchService : IUserApiKeySearchService
    {
        private readonly Func<ISecurityRepository> _repositoryFactory;

        public UserApiKeySearchService(Func<ISecurityRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<UserApiKeySearchResult> SearchUserApiKeysAsync(UserApiKeySearchCriteria criteria)
        {
            using (var repository = _repositoryFactory())
            {
                if (criteria == null)
                {
                    throw new ArgumentNullException(nameof(criteria));
                }

                var result = AbstractTypeFactory<UserApiKeySearchResult>.TryCreateInstance();

                var query = repository.UserApiKeys.AsNoTracking();
                result.TotalCount = await query.CountAsync();

                var sortInfos = criteria.SortInfos;
                if (sortInfos.IsNullOrEmpty())
                {
                    sortInfos = new[] { new SortInfo { SortColumn = ReflectionUtility.GetPropertyName<UserApiKey>(x => x.ApiKey), SortDirection = SortDirection.Ascending } };
                }
                var apiKeysEntities = await query.OrderBySortInfos(sortInfos).Skip(criteria.Skip).Take(criteria.Take).ToArrayAsync();
                result.Results = apiKeysEntities.Select(x => x.ToModel(AbstractTypeFactory<UserApiKey>.TryCreateInstance())).ToArray();

                return result;
            }
        }
    }
}
