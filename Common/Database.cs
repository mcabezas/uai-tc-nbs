using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public class Database : IDatabase
    {
        private readonly SqlConnection _sqlConnection;

        public Database()
        {
        }
        
        public Database(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public SqlTransaction BeginTransaction()
        {
            ConnectionMustBeOpen();
            return _sqlConnection.BeginTransaction();
        }

        
        public object ExecuteScalar(string sqlStatement, SqlParameter[] parameters)
        {
            ConnectionMustBeOpen();
            var sqlTransaction = _sqlConnection.BeginTransaction();
            var result = ExecuteScalar(sqlStatement, parameters, sqlTransaction);
            sqlTransaction.Commit();
            return result;
        }
        
        public object ExecuteScalar(string sqlStatement, SqlParameter[] parameters, SqlTransaction transaction)
        {
            ConnectionMustBeOpen();
            var command = new SqlCommand(sqlStatement, _sqlConnection, transaction);
            command.Parameters.AddRange(parameters);
            return command.ExecuteScalar();
        }

        public void ExecuteNonQuery(string sqlStatement, SqlParameter[] parameters)
        {
            ConnectionMustBeOpen();
            var sqlTransaction = _sqlConnection.BeginTransaction();
            ExecuteNonQuery(sqlStatement, parameters, sqlTransaction);
            sqlTransaction.Commit();
        }

        public void ExecuteNonQuery(string sqlStatement, SqlParameter[] parameters, SqlTransaction transaction)
        {
            ConnectionMustBeOpen();
            var command = new SqlCommand(sqlStatement, _sqlConnection, transaction);
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
        }
        
        public DataSet ExecuteQuery(string sqlStatement, SqlParameter[] parameters)
        {
            ConnectionMustBeOpen();
            var command = new SqlCommand(sqlStatement, _sqlConnection);
            command.Parameters.AddRange(parameters);
            var dataAdapter = new SqlDataAdapter();
            var dataSet = new DataSet();
            
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataSet, "result");
            
            return dataSet;
        }
        
        private void ConnectionMustBeOpen()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }

    }
}