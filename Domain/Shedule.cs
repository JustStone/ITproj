using System;
namespace Domain
{
    public class Shedule
    {
        public Shedule() { }
        public int id;
        public DateTime startwork;
        public DateTime endwork;

        public int ID
        {
            get { return id; }
        }

        public DateTime StartWork
        {
            get { return startwork; }
            set { startwork = value; }
        }

        public DateTime EndWork
        {
            get { return endwork; }
            set { endwork = value; }
        }
    }
}

