using System.Threading.Tasks;

internal interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}