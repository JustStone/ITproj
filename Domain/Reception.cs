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

        public DateTime StartRecept { get; set; }

        public DateTime EndRecept { get; set; }

        public int IDPatient { get; set; }

        public int IDDoctor { get; set; }
    }
}

