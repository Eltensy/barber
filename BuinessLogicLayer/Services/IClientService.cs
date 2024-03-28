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
        List<ClientDto> GetClients();
        ClientDto GetClientById(int clientId);
        void InsertClient(ClientDto clientDto);
        void DeleteClient(int clientId);
        void UpdateClient(ClientDto clientDto);
    }
}
