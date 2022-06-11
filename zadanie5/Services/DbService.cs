using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zadanie5.Models;
using zadanie5.Models.DTO;

namespace zadanie5.Services
{
    public class DbService : IDbService
    {
        private readonly _2019SBDContext _dbContext;
        public DbService(_2019SBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddClientForTrip(Order order)
        {

            //Czy wycieczka istnieje
            if(_dbContext.Trips.Any(e => e.IdTrip == order.IdTrip))
            {
                Console.WriteLine("Tu wchodze jesli wycieczka istnieje");
                 
            }else
            {
                return false;
            }

            if (_dbContext.Clients.Any(e => e.Pesel == order.Pesel))
            {
                Console.WriteLine("Taki klient juz istnieje");
            }
            else
            {
                Console.WriteLine("Dodaje nowego klienta");
                var client = new Client(order.FirstName, order.LastName, order.Email, order.Telephone, order.Pesel);
                _dbContext.Add<Client>(client);
                _dbContext.SaveChanges();

            }

            var currentClient = _dbContext.Clients.Where(e => e.Pesel == order.Pesel).FirstOrDefault();
            int idClienta = currentClient.IdClient;

            Console.WriteLine(idClienta + " a tu id wycieczki" + order.IdTrip);
            if (_dbContext.ClientTrips.Any(e => e.IdTrip == order.IdTrip && e.IdClient == idClienta))
            {
                Console.WriteLine("Dnay uczestnik jest juz zapisany na tą wycieczkę");
                return false;

            }
            else
            {
                Console.WriteLine("id clienta:" + idClienta + " id wyczieczki: " + order.IdTrip + " pd" + order.PaymentDate);
              
                var startOrder = new ClientTrip(idClienta, order.IdTrip, order.PaymentDate);
               _dbContext.Add<ClientTrip>(startOrder);
               _dbContext.SaveChanges();
                return true;
            }


      

           



        }

        public bool DeleteClient(int idClient)
        {
            try
            {
                if (_dbContext.ClientTrips.Any(e => e.IdClient == idClient))
                {
                    return false;
                }
                else
                {
                    _dbContext.Remove(_dbContext.Clients.Single(e => e.IdClient == idClient));
                    _dbContext.SaveChanges();
                    return true;
                }
            }catch (Exception ex)
            {
                return false;
            }


          
        }

        public async Task<IEnumerable<SomeSortOfTrips>> GetTrips()
        {
            return await _dbContext.Trips
                .Include(e => e.CountryTrips)
                .Include(e => e.ClientTrips)
                .Select(e => new SomeSortOfTrips
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select(e => new SomeSortOfCountry { Name = e.IdCountryNavigation.Name }).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName })
                }).OrderByDescending(e => e.DateFrom)
                .ToListAsync();



        }
    }
}
