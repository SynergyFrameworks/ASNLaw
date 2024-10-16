using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Common.Events;
using Infrastructure.Security;
using Infrastructure.Security.Events;

namespace Infrastructure.Security.Handlers
{
    public class UserApiKeyActualizeEventHandler : IEventHandler<UserChangedEvent, CancellationToken>
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
                        await _userApiKeyService.DeleteApiKeysAsync(allUserApiKeys.Select(x => x.Id.ToString()).ToArray());
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
