using Electronics.Core.Interfaces;
using Electronics.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics.Core.Services
{
    public class ElectronicClient : IElectronicClient
    {
        public async Task<List<ElectronicModel>> GetItemsAsync()
        {
            List<ElectronicModel> items = new List<ElectronicModel>
            {
                new ElectronicModel
                {
                    Name = "TV",
                    Price = 1000
                },
                new ElectronicModel
                {
                    Name = "PC",
                    Price = 2000
                }
            };

            return items;
        }
    }
}
