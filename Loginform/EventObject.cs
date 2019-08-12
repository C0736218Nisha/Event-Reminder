using System;
namespace Loginform
{
    public class EventObject
    {
        public String name, createdby, sentto, date, details, accepted;


        public EventObject(String createdby, String sentto, String name, String details, String date, String accepted)
        {
            this.name = name;
            this.createdby = createdby;
            this.sentto = sentto;
            this.date = date;
            this.details = details;
            this.accepted = accepted;
        }
    }
}
