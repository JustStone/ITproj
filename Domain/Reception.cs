using System;
namespace Domain
{
    public class Reception
    {
        public Reception() { }
        public DateTime startrecept;
        public DateTime endrecept;
        public Shedule shedule;
        public int idpatient;
        public int iddoctor;

        public DateTime StartRecept
        {
            get { return startrecept; }
            set { startrecept = value; }
        }

        public DateTime EndRecept
        {
            get { return endrecept; }
            set { endrecept = value; }
        }

        public int IDPatient
        {
            get { return idpatient; }
        }

        public int IDDoctor
        {
            get { return iddoctor; }
        }

        public Shedule Shedule(int ID, DateTime date)
        {
            Shedule shedulearr = null;
            return shedulearr;
        }

        public void SaveRecord(int id)
        {

        }

        public DateTime[] FreeDates(string specialization)
        {
            DateTime[] freedatearr = new DateTime[100];
            return freedatearr;
        }

        public void ChangeShedule (int id, DateTime startwork, DateTime endwork)
        {
        }



    }
}

