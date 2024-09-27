using System.Collections.Generic;

namespace com.ethnicthv.Script
{
    public abstract class Event
    {
        
        public readonly int EventId;

        protected Event(int eventId)
        {
            EventId = eventId;
        }
    }
}