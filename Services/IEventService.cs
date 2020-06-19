using LostArkEng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAE.Services
{
    public interface IEventService
    {
        IEnumerable<EventType> GetEvents();
        IEnumerable<Activity> GetSubEvents(int categoryId);

    }
}
