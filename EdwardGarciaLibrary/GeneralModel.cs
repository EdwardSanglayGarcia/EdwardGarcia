using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdwardGarciaLibrary
{
    public class MailList
    {
        public int MailID { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CurrentStatusID { get; set; }

    }

    public class MasterAdministrator
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class MailStatus
    {
        public int MailStatusID { get; set; }
        public string Description { get; set; }
    }
}
