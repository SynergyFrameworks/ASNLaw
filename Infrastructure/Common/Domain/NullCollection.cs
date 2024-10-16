using System.Collections.ObjectModel;

namespace Infrastructure.Common
{
    //Used for mark that collection is null (ot initialized) used in pacth operation
    public class NullCollection<T> : ObservableCollection<T>
    {
    }
}
