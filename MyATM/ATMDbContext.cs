using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace MyATM
{
    public class ATMDbContext : IDisposable
    {
        private readonly string _connectionString;
        private bool _disposed;
        private SqlConnection _dbconnection = null;

        public ATMDbContext() : this(@"Data Source=DESKTOP-2OA94O7\SQLEXPRESS;Initial Catalog=ATMachine;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public ATMDbContext(string connString)
        {
            _connectionString = connString; 
        }

        public SqlConnection OpenConn()
        {
            _dbconnection = new SqlConnection(_connectionString);
            _dbconnection.Open();
            return _dbconnection;
        }

        public void CloseConnection()
        {
            if(_dbconnection?.State !=ConnectionState.Closed) _dbconnection?.Close();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbconnection.Dispose();
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
