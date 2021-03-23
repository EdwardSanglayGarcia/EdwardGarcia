using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdwardGarciaLibrary
{
    public interface IDataProvider
    {
        List<MailStatus> GetStatus();

        List<MailList> InsertMailList(MailList mailList);

        List<MailList> GetMailList(int CurrentStatusID);

        List<MailList> UpdateMailList(MailList mailList);

        List<MasterAdministrator> GetAccount();

    }
}
