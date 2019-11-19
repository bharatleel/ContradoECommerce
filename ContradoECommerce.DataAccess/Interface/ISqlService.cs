using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ContradoECommerce.DataAccess.Interface
{
    public interface ISqlService
    {
        int CommandTimeOut { get; set; }

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters);

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connectionString.</param>
        /// <returns></returns>
        Task ExecuteStoredProcedureAsync(string storedProcedureName, SqlParameter[] parameters, string connectionString);

        /// <summary>
        /// Executes the stored procedure and return object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<T> ExecuteStoredProcedureAndReturnObjectAsync<T>(string storedProcedureName, SqlParameter[] parameters);

        /// <summary>
        /// Executes the stored procedure and return list object asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<List<T>> ExecuteStoredProcedureAndReturnListObjectAsync<T>(string storedProcedureName, SqlParameter[] parameters);

        /// <summary>
        /// Executes the stored procedure and return data table asynchronous.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<DataTable> ExecuteStoredProcedureAndReturnDataTableAsync(string storedProcedureName, SqlParameter[] parameters);

        /// <summary>
        /// Executes the stored procedure and return data table asynchronous.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Task<DataTable> ExecuteStoredProcedureAndReturnDataTableAsync(string storedProcedureName, SqlParameter[] parameters, string connectionString);

        /// <summary>
        /// Executes the text command and return data table asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<DataTable> ExecuteTextCommandAndReturnDataTableAsync(string commandText, SqlParameter[] parameters);

        /// <summary>
        /// Executes the text command and return list object asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Task<List<T>> ExecuteTextCommandAndReturnListObjectAsync<T>(string commandText, SqlParameter[] parameters, string connectionString);
        /// <summary>
        /// Executes the text command and return data table asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Task<DataTable> ExecuteTextCommandAndReturnDataTableAsync(string commandText, SqlParameter[] parameters, string connectionString);

        /// <summary>
        /// Executes the text command and return scalar result asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Task<object> ExecuteTextCommandAndReturnScalarResultAsync(string commandText, SqlParameter[] parameters, string connectionString);
        /// <summary>
        /// Executes the text command and return scalar result asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<object> ExecuteTextCommandAndReturnScalarResultAsync(string commandText, SqlParameter[] parameters);

        /// <summary>
        /// Executes the text command asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        Task ExecuteTextCommandAsync(string commandText, SqlParameter[] parameters, string connectionString);
        /// <summary>
        /// Executes the text command asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task ExecuteTextCommandAsync(string commandText, SqlParameter[] parameters);
        /// <summary>
        /// Executes the text command asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        Task ExecuteTextCommandAsync(string commandText);
        /// <summary>
        /// Gets schema table by executing the query asynchronously.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="connectionString">The data source connection string</param>
        /// <returns></returns>
        Task<DataTable> ExecuteTextCommandAndReturnSchemaTableAsync(string commandText, SqlParameter[] parameters, string connectionString);
        /// <summary>
        /// The function that parses and returns the connection string equivalent to the sql authenticaiton from the connection string having windows authentication
        /// </summary>
        /// <param name="connectionStringWithWindowsAuthentication">the connection string with windows authentication</param>
        /// <returns>the connection string with SQL Authentication</returns>
        string ParseConnectionStringWithSQLAuthentication(string connectionStringWithWindowsAuthentication);
        /// <summary>
        /// The function that check the Database Connection with provided Connectionstring
        /// </summary>
        /// <param name="connectionString">Connectionstring to Database Connection</param>
        /// <returns></returns>
        Task<bool> CheckSQLDatabaseConnection(string connectionString);
    }
}