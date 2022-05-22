using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using System;
using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class PostgreSqlLogger : LoggerServiceBase
    {
        public PostgreSqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:PostgreConfiguration")
                .Get<PostgreConfiguration>() ?? throw new Exception(Utilities.Messages.SerilogMessages.NullOptionsMessage);

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
                        {
                            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                            {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                        };


            var seriLogConfig = new LoggerConfiguration()
                    .WriteTo.PostgreSQL(connectionString: logConfig.ConnectionString, tableName: "logs", columnWriters, needAutoCreateTable: true)
                    .CreateLogger();
            Logger = seriLogConfig;
        }
    }
}
