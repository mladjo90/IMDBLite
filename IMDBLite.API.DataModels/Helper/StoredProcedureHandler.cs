using IMDBLite.API.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDBLite.API.DataModels.Helper
{
    public static class StoredProcedureHandler
    {
        private static readonly string FREE_USER_PROCEDURE = "FindFreeUser";

        public static async Task<string> FindFreeUser(string connectionString)
        {
            using (var connection = new MySqlDatabase(connectionString))
            {
                if (connection.Connection.State == System.Data.ConnectionState.Open)
                {
                     return await RunInsertTicketQuery(connection, FREE_USER_PROCEDURE);
                }
                else
                {
                    throw new Exception($"Error opening connection to MySQL");
                }
            }
        }

        private static async Task<string> RunInsertTicketQuery(MySqlDatabase connection, string procedureName)
        {
            string UserName = "";
            using (var transaction = await connection.Connection.BeginTransactionAsync())
            {
                    using (var findFreeNameUser = new MySqlCommand(procedureName, connection.Connection))
                    {
                        findFreeNameUser.CommandType = System.Data.CommandType.StoredProcedure;
                            
                        findFreeNameUser.Parameters.Add("@success_param", MySqlDbType.Int32);
                        findFreeNameUser.Parameters.Add("@pUserName", MySqlDbType.VarChar);

                        findFreeNameUser.Parameters["@success_param"].Direction = System.Data.ParameterDirection.Output;
                        findFreeNameUser.Parameters["@pUserName"].Direction = System.Data.ParameterDirection.Output;

                        await findFreeNameUser.ExecuteNonQueryAsync();

                        int successParam = (int)findFreeNameUser.Parameters["@success_param"].Value;
                        UserName = findFreeNameUser.Parameters["@pUserName"].Value == DBNull.Value ? string.Empty : (string)findFreeNameUser.Parameters["@pUserName"].Value;

                        if (successParam != 1)
                        {
                            await transaction.DisposeAsync();
                            throw new Exception("Error with procedure getFreeUserName");
                        }
                    }
                await transaction.CommitAsync();
                return UserName;
            }
        }

    }
}