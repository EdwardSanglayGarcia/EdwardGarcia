using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdwardGarciaLibrary
{
    using Dapper;
    using System.Data;
    using System.Data.SqlClient;

    public class DataProvider : DataAccess, IDataProvider
    {

        public List<MailList> InsertMailList(MailList mailList)
        {
            var result = new List<MailList>();
            using (IDbConnection con = new SqlConnection(constring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@Sender", mailList.Sender);
                param.Add("@Subject", mailList.Subject);
                param.Add("@Message", mailList.Message);

                result = con.Query<MailList>(
                    StoredProcedure.I_MailList.ToString(), param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public List<MailList> UpdateMailList(MailList mailList)
        {
            var result = new List<MailList>();
            using (IDbConnection con = new SqlConnection(constring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@CurrentStatusID", mailList.CurrentStatusID);
                param.Add("@MailID", mailList.MailID);

                result = con.Query<MailList>(
                    StoredProcedure.U_MailList.ToString(), param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public List<MailList> GetMailList(int CurrentStatusID)
        {
            var result = new List<MailList>();
            using (IDbConnection con = new SqlConnection(constring))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@CurrentStatusID", CurrentStatusID);

                result = con.Query<MailList>(
                    StoredProcedure.V_MailList.ToString(), param, commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public List<MailStatus> GetStatus()
        {
            var result = new List<MailStatus>();
            using (IDbConnection con = new SqlConnection(constring))
            {
                con.Open();
                result = con.Query<MailStatus>(
                    StoredProcedure.V_MailStatus.ToString(), commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }

        public List<MasterAdministrator> GetAccount()
        {
            var result = new List<MasterAdministrator>();
            using (IDbConnection con = new SqlConnection(constring))
            {
                con.Open();
                result = con.Query<MasterAdministrator>(
                    StoredProcedure.V_MasterAdministrator.ToString(), commandType: CommandType.StoredProcedure).ToList();
            }
            return result;
        }



    }
}