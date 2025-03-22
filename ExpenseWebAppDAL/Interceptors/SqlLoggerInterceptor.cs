using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebAppDAL.Interceptors
{
    internal class SqlLoggerInterceptor : DbCommandInterceptor
    {
        private static readonly string _logFilePath = "sql_logs.txt";
        public override async ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {

            LogToFile("=========================[Executed]=========================\n"
                + command.CommandText
                + "\n=========================[Executed]=========================");

            return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override void CommandFailed(
            DbCommand command,
            CommandErrorEventData eventData)
        {
            LogToFile("=========================[Error]=========================\n"
                + $"{command.CommandText} \n Error: {eventData.Exception.Message}\n"
                + "=========================[Error]=========================");

            base.CommandFailed(command, eventData);
        }
        private static void LogToFile(string message)
        {
            try
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] \n {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging error: {ex.Message}");
            }
        }
    }
}
