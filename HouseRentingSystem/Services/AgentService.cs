using HouseRentingSystem.Contracts.Agent;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services
{
    public class AgentService : IAgentService
    {
        private readonly HouseRentingDbContext _data;
        public AgentService(HouseRentingDbContext data)
        {
            _data = data;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await _data.Agents.AddAsync(agent);
            await _data.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string userId)
        {
            return await _data.Agents.AnyAsync(a => a.UserId == userId);
        }

        public async Task<int?> GetAgentId(string userId)
        {
            return (await _data.Agents.FirstOrDefaultAsync(a => a.UserId == userId))?.Id;
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return await _data.Houses.AnyAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await _data.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
