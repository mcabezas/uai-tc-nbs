using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public interface IDatabase
    {
        SqlTransaction BeginTransaction();

        object ExecuteScalar(string sqlStatement, SqlParameter[] parameters);
        object ExecuteScalar(string sqlStatement, SqlParameter[] parameters, SqlTransaction transaction);
        void ExecuteNonQuery(string sqlStatement, SqlParameter[] parameters);
        void ExecuteNonQuery(string sqlStatement, SqlParameter[] parameters, SqlTransaction transaction);
        
        DataSet ExecuteQuery(string sqlStatement, SqlParameter[] parameters);
    }
}