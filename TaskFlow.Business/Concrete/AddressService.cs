using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.DataAccess.Abstract;
using TaskFlow.Entities.Models;

namespace TaskFlow.DataAccess.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IAddressDal dal;

        public AddressService(IAddressDal dal)
        {
            this.dal = dal;
        }

        public async System.Threading.Tasks.Task Add(Address address)
        {
           await dal.Add(address);
        }

        public async System.Threading.Tasks.Task Delete(Address address)
        {
          await dal.Delete(address);
        }

        public async Task<Address> GetAddressById(int id)
        {
          return await dal.GetById(f=>f.Id == id);
        }

        public async Task<List<Address>> GetAddresses()
        {
           return await dal.GetAll();
        }

        public async System.Threading.Tasks.Task Update(Address address)
        {
            await dal.Update(address);  
        }
    }
}
