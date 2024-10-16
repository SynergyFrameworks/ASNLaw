using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Events;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Events;

namespace Scion.Business.Security.Handlers
{
    public class UserApiKeyActualizeEventHandler : IEventHandler<UserChangedEvent>
    {
        private readonly IUserApiKeyService _userApiKeyService;
        public UserApiKeyActualizeEventHandler(IUserApiKeyService userApiKeyService)
        {
            _userApiKeyService = userApiKeyService;
        }

        public virtual async Task Handle(UserChangedEvent message)
        {
            foreach (var changedEntry in message.ChangedEntries)
            {
                if(changedEntry.EntryState == EntryState.Deleted)
                {
                    var allUserApiKeys = await _userApiKeyService.GetAllUserApiKeysAsync(changedEntry.OldEntry.Id);
                    if(allUserApiKeys != null)
                    {
                        await _userApiKeyService.DeleteApiKeysAsync(allUserApiKeys.Select(x => x.Id).ToArray());
                    }
                }                
            }
        }

        public Task Handle(UserChangedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
