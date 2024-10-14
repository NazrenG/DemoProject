using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Abstract
{
    public interface IAddressService
    {
        Task<List<Address>> GetAddresses();
        Task<Address> GetAddressById(int id);
        Task Add(Address address);
        Task Update(Address address);
        Task Delete(Address address);

    }
}
