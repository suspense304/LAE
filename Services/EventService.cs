using LAE.Data;
using LostArkEng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAE.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(LAE.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EventType> GetEvents()
        {
            List<EventType> eventList = new List<EventType>();
            eventList = _context.EventType.ToList();
            return eventList;
        }
            
        
        public IEnumerable<Activity> GetSubEvents(int typeId)
        {
            var subCategories = new List<Activity>();
            subCategories = _context.Actvity.AsQueryable().Where(s => s.TypeId == typeId).OrderBy(w=> w.Name).ToList();
            return subCategories;
        }
    }
}
