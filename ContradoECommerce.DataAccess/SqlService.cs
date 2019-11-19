using ContradoECommerce.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ContradoECommerce.DataAccess
{
    public class SqlService : ISqlService
    {
        #region Private Variables
        private readonly string _strConnectionString;
        private int _commandTimeOut = -1;
        private bool _isHexadecimalInXml = false;
        #endregion


        public int CommandTimeOut
        {
            get
            {
                return _commandTimeOut;
            }
            set
            {
                _commandTimeOut = value;
            }
        }

        public SqlService(string strConnectionString)
        {
            //get tenant connection string. 
            _strConnectionString = strConnectionString;
        }

        #region Public Methods
        public async Task ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters)
        {
            await ExecuteStoredProcedureAsync(storedProcedureName, parameters, this._strConnectionString);
        }

        public async Task ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters, string strConnectionString)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(strConnectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, cn))
                    {
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                            foreach (SqlParameter currentParameter in parameters)
                                cmd.Parameters.Add(currentParameter);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<List<T>> ExecuteStoredProcedureAndReturnListObjectAsync<T>(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                List<T> dataToReturn;
                using (SqlConnection cn = new SqlConnection(this._strConnectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, cn))
                    {
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                            foreach (SqlParameter currentParameter in parameters)
                                cmd.Parameters.Add(currentParameter);
                        SqlDataReader dtReader = await cmd.ExecuteReaderAsync();
                        dataToReturn = await new GenericPopulator<T>().PopulateList(dtReader);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<T> ExecuteStoredProcedureAndReturnObjectAsync<T>(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                T dataToReturn;
                using (SqlConnection cn = new SqlConnection(this._strConnectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, cn))
                    {
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (parameters != null)
                            foreach (SqlParameter currentParameter in parameters)
                                cmd.Parameters.Add(currentParameter);
                        SqlDataReader dtReader = await cmd.ExecuteReaderAsync();
                        dataToReturn = await new GenericPopulator<T>().Populate(dtReader);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }

            }
        }


        public async Task<DataTable> ExecuteStoredProcedureAndReturnDataTableAsync(string storedProcedureName, SqlParameter[] parameters)
        {
            var result = await ExecuteStoredProcedureAndReturnDataTableAsync(storedProcedureName, parameters, _strConnectionString);
            return (DataTable)result;
        }

        public async Task<DataTable> ExecuteStoredProcedureAndReturnDataTableAsync(string storedProcedureName, SqlParameter[] parameters, string connectionString)
        {
            var result = await CreateAndExecuteCommand(storedProcedureName, connectionString, parameters, CommandType.StoredProcedure, ReturnType.DataTable);
            return (DataTable)result;
        }

        public async Task<DataTable> ExecuteTextCommandAndReturnDataTableAsync(string commandText, SqlParameter[] parameters)
        {
            var result = await ExecuteTextCommandAndReturnDataTableAsync(commandText, parameters, _strConnectionString);
            return (DataTable)result;
        }
        public async Task<List<T>> ExecuteTextCommandAndReturnListObjectAsync<T>(string commandText, SqlParameter[] parameters, string connectionString)
        {

            try
            {
                List<T> dataToReturn;
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    await cn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(commandText, cn))
                    {
                        if (_commandTimeOut != -1)
                            cmd.CommandTimeout = _commandTimeOut;
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (parameters != null)
                            foreach (SqlParameter currentParameter in parameters)
                                cmd.Parameters.Add(currentParameter);
                        SqlDataReader dtReader = await cmd.ExecuteReaderAsync();
                        dataToReturn = await new GenericPopulator<T>().PopulateList(dtReader);
                    }
                }
                return dataToReturn;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<DataTable> ExecuteTextCommandAndReturnDataTableAsync(string commandText, SqlParameter[] parameters, string connectionString)
        {
            var result = await CreateAndExecuteCommand(commandText, connectionString, parameters, CommandType.Text, ReturnType.DataTable);
            return (DataTable)result;
        }

        public async Task<object> ExecuteTextCommandAndReturnScalarResultAsync(string commandText, SqlParameter[] parameters)
        {
            var result = await ExecuteTextCommandAndReturnScalarResultAsync(commandText, parameters, _strConnectionString);
            return result;
        }

        public async Task<object> ExecuteTextCommandAndReturnScalarResultAsync(string commandText, SqlParameter[] parameters, string connectionString)
        {
            var result = await CreateAndExecuteCommand(commandText, connectionString, parameters, CommandType.Text, ReturnType.ScalarResult);
            return result;
        }

        public async Task ExecuteTextCommandAsync(string commandText)
        {
            await ExecuteTextCommandAsync(commandText, null);
        }

        public async Task ExecuteTextCommandAsync(string commandText, SqlParameter[] parameters)
        {
            await ExecuteTextCommandAsync(commandText, parameters, _strConnectionString);
        }

        public async Task ExecuteTextCommandAsync(string commandText, SqlParameter[] parameters, string connectionString)
        {
            await CreateAndExecuteCommand(commandText, connectionString, parameters, CommandType.Text, ReturnType.None);
        }

        #endregion


        #region Private Methods
        private enum ReturnType : byte
        {
            ScalarResult,
            DataTable,
            DataSet,
            None,
            Xml,
            Schema,
            JSON,
            DataReader
        }

        private async Task<object> CreateAndExecuteCommand(string StoredProcedureName, string ConnectionString, SqlParameter[] Parameters, CommandType TypeOfCommand, ReturnType TypeToReturn)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand targetCommand = new SqlCommand(StoredProcedureName, con))
                    {
                        targetCommand.CommandType = TypeOfCommand;
                        if (_commandTimeOut != -1)
                            targetCommand.CommandTimeout = _commandTimeOut;
                        if (Parameters != null)
                            foreach (SqlParameter currentParameter in Parameters)
                                targetCommand.Parameters.Add(currentParameter);
                        //Trace.WriteLine("return from create and execute command.");
                        return await ExecuteCommand(targetCommand, TypeToReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }
            }

        }

        private async Task<object> ExecuteCommand(SqlCommand commandToExecute, ReturnType returnValType)
        {
            switch (returnValType)
            {
                case ReturnType.ScalarResult:
                    object returnObject = await commandToExecute.ExecuteScalarAsync();
                    if (returnObject != null && returnObject.GetType() != typeof(System.DBNull))
                        return returnObject;
                    else
                        return null;
                case ReturnType.None:
                    return await commandToExecute.ExecuteNonQueryAsync();
                case ReturnType.DataTable:
                    goto case ReturnType.DataSet;
                case ReturnType.DataSet:
                    SqlDataAdapter returnValDBAdapter = null;
                    try
                    {
                        returnValDBAdapter = new SqlDataAdapter(commandToExecute);
                        if (returnValType == ReturnType.DataSet)
                        {
                            DataSet returnValDS = new DataSet("Results");
                            returnValDBAdapter.Fill(returnValDS);
                            return returnValDS;
                        }
                        else
                        {
                            DataTable returnValDT = new DataTable("Results");
                            returnValDBAdapter.Fill(returnValDT);
                            return returnValDT;
                        }
                    }
                    finally
                    {
                        returnValDBAdapter.Dispose();
                    }
                case ReturnType.Xml:
                    try
                    {
                        if (!_isHexadecimalInXml)
                        {
                            string strReturnResult = string.Empty;
                            using (XmlReader reader = await commandToExecute.ExecuteXmlReaderAsync())
                            {
                                reader.Read();
                                strReturnResult = await reader.ReadOuterXmlAsync();
                                reader.Close();
                            }
                            return strReturnResult;
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            try
                            {
                                using (SqlDataReader reader = await commandToExecute.ExecuteReaderAsync())
                                {
                                    while (reader.Read())
                                        sb.Append((string)reader[0]);
                                }

                                return sb.ToString();
                            }
                            finally
                            {
                                sb = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToLower().Contains("hexadecimal"))
                        {
                            _isHexadecimalInXml = true;
                            StringBuilder sb = new StringBuilder();
                            try
                            {
                                using (SqlDataReader reader = await commandToExecute.ExecuteReaderAsync())
                                {
                                    while (reader.Read())
                                        sb.Append((string)reader[0]);
                                }

                                return sb.ToString();
                            }
                            finally
                            {
                                sb = null;
                            }
                        }
                        else
                            throw;
                    }

                case ReturnType.Schema:
                    returnValDBAdapter = new SqlDataAdapter(commandToExecute);
                    DataTable returnValDTSchema = new DataTable("Results");
                    returnValDBAdapter.FillSchema(returnValDTSchema, SchemaType.Mapped);
                    return returnValDTSchema;
                default:
                    throw new ApplicationException("Invalid Return Type");
            }
        }

        public async Task<DataTable> ExecuteTextCommandAndReturnSchemaTableAsync(string commandText, SqlParameter[] parameters, string connectionString)
        {
            var result = await CreateAndExecuteCommand(commandText, connectionString, parameters, CommandType.Text, ReturnType.Schema);
            return (DataTable)result;
        }

        #endregion

        public string ParseConnectionStringWithSQLAuthentication(string connectionStringWithWindowsAuthentication)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionStringWithWindowsAuthentication))
                    throw new ArgumentException("The connection string parameter is empty.", "connectionStringWithWindowsAuthentication");
                else if (connectionStringWithWindowsAuthentication.Equals("PAPERSAVEDATABASE", StringComparison.InvariantCultureIgnoreCase))
                    return this._strConnectionString;
                else
                {
                    SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionStringWithWindowsAuthentication);
                    if (sqlConnectionStringBuilder.IntegratedSecurity == true && sqlConnectionStringBuilder.InitialCatalog.ToLower().StartsWith("papersave"))
                    {
                        SqlConnectionStringBuilder paperSaveConnectionStringBuilder = new SqlConnectionStringBuilder(this._strConnectionString);
                        sqlConnectionStringBuilder.DataSource = paperSaveConnectionStringBuilder.DataSource;
                        sqlConnectionStringBuilder.UserID = paperSaveConnectionStringBuilder.UserID;
                        sqlConnectionStringBuilder.Password = paperSaveConnectionStringBuilder.Password;
                        sqlConnectionStringBuilder.IntegratedSecurity = paperSaveConnectionStringBuilder.IntegratedSecurity;
                    }
                    return sqlConnectionStringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("keyword not supported"))
                {
                    throw new Exception("It seems you have used some special characters in your password like semicolon (;). If it is the case, enter your password within the single quote (') like 'Password'.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> CheckSQLDatabaseConnection(string connectionString)
        {
            string errorMessage = string.Empty;
            bool sqlConnectionResult = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    if (conn != null && conn.State != ConnectionState.Open)
                    {
                        errorMessage = "Connection is already closed.";
                        sqlConnectionResult = false;
                    }
                    else if (conn != null)
                        sqlConnectionResult = true;
                    else
                        sqlConnectionResult = false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while attempting to test the connection string setting for the selected company. " + errorMessage
                    + " Double check that you have the proper connection string and if you believe the PaperSave Data Access Web Service component needs to be used for the Company's data access, then attempt your operation again with configuring the Data Access Service related inputs."
                    + " If you cannot then try to close this window and re-attempt your operation.  Contact PaperSave Support if this issue persists. You may not be able to update the related company until the underlying issue is resolved." + Environment.NewLine + "Error:" + ex.Message;
                throw new Exception(errorMessage);
            }
            return sqlConnectionResult;
        }
    }
}
