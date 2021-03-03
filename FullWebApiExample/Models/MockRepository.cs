using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FullWebApiExample.Models
{
    public class MockRepository : IRepository
    {

        private Dictionary<int, Reservation> items;
        public MockRepository()
        {
            items = new Dictionary<int, Reservation>();
            new List<Reservation>
            {
                new Reservation {Id = 1, Name = "Mudasir", StartLocation = "Kashmir", EndLocation = "Everest"},
                new Reservation {Id = 2, Name = "Mussaib", StartLocation = "delhi", EndLocation = "New York"},
                new Reservation {Id = 3, Name = "Ahmad", StartLocation = "Aligarh", EndLocation = "USA"}

            }.ForEach(r => AddReservation(r));
        }

        // this is for [httpget("{id}")] to retturn perticular id item
        public Reservation this[int id] => items.ContainsKey(id)? items[id]:null;
        // this is for [httpget] to return all items
        public IEnumerable<Reservation> Reservations => items.Values;
        // adding new record to the Reservation
        public Reservation AddReservation(Reservation reservation)
        {
            int key = items.Count();
            //Debug.Write(key);
            while (items.ContainsKey(key))
            {
                key++;
                reservation.Id = key;
                
            }
            
            items[reservation.Id] = reservation;
            return reservation;
        }

        public void DeleteReservation(int id) => items.Remove(id);

        
       

        public Reservation Details(Reservation reservation)
        {
            if(reservation.Id != 0)
            {
                reservation = items[reservation.Id];
            }
            return reservation;
        }

        public Reservation UpdateReservation(Reservation reservation) => AddReservation(reservation);


        
    }
}
