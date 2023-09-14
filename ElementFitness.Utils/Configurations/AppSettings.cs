using Microsoft.Extensions.Configuration;


namespace ElementFitness.Utils.Configurations
{
    public static class AppSettings
    {
        private static IConfiguration? Configuration;

        public static void IntializeConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string PostGresConnectionString
        {
            get
            {
                if (Configuration["ConnectionStrings:PostgresConnectionString"] == null || string.IsNullOrWhiteSpace(Configuration["ConnectionStrings:PostgresConnectionString"]))
                    throw new Exception("Postgres Connection String is not valid in AppSettings file.");
                return Configuration["ConnectionStrings:PostgresConnectionString"];
            }
        }

        public static string SerilogOutputFilePath
        {
            get
            {
                if (Configuration["Serilog:outputFilePath"] == null || string.IsNullOrWhiteSpace(Configuration["Serilog:outputFilePath"]))
                    throw new Exception("There is no output file path defined for Serilog.");
                return Configuration["Serilog:outputFilePath"];
            }
        }

    }
}



