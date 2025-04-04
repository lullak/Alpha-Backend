using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories
{
    public interface IUserAddressRepository : IBaseRepository<UserAddressEntity>
    {
    }
    public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context), IUserAddressRepository
    {
    }

}
