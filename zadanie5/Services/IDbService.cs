using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using zadanie5.Models;
using zadanie5.Models.DTO;

namespace zadanie5.Services
{
    public interface IDbService
    {

        Task<IEnumerable<SomeSortOfTrips>> GetTrips();
         bool DeleteClient(int idClienta);
        bool AddClientForTrip(Order order);
    }
}
