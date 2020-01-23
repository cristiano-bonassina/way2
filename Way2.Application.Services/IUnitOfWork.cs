using System.Threading.Tasks;

namespace Way2.Application.Services
{
    public interface IUnitOfWork
    {

        Task BeginTransactionAsync();

        Task EndTransactionAsync(bool commit = true);

    }
}
