using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace QNAForum.Core.DataAccess.ADO.Net
{
    public class QNAForumDBContext: IQNAForumDBContext
    {
        private readonly SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;

        public SqlCommand SqlCommand
        {
            get { return _sqlCommand; }

            set { _sqlCommand = value; }
        }
        public QNAForumDBContext()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SampleDBConnStr"].ConnectionString);
            _sqlCommand.Connection = _sqlConnection;

        }
    }
}
