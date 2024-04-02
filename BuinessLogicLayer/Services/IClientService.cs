using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetClients();
        Task<ClientDto?> GetClientById(int clientId);
        Task InsertClient(ClientDto clientDto);
        Task DeleteClient(int clientId);
        Task UpdateClient(ClientDto clientDto);
        Task<ClientDto?> GetClientByEmail(string email);
    }
}
