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

        public static string AdminPassword
        {
            get
            {
                if (Configuration["AdminPassword"] == null || string.IsNullOrWhiteSpace(Configuration["AdminPassword"]))
                    throw new Exception("There is no admin password defined.");
                return Configuration["AdminPassword"];
            }
        }

        public static string FacilitiesImagesLimit
        {
            get
            {
                if (Configuration["FacilitiesImagesLimit"] == null || string.IsNullOrWhiteSpace(Configuration["FacilitiesImagesLimit"]))
                    throw new Exception("There is no facilities images limit defined.");
                return Configuration["FacilitiesImagesLimit"];
            }
        }


        public static string MailjetPublicKey
        {
            get
            {
                if (Configuration["Mailjet:PublicKey"] == null || string.IsNullOrWhiteSpace(Configuration["Mailjet:PublicKey"]))
                    throw new Exception("There is no mailjet public key defined.");
                return Configuration["Mailjet:PublicKey"];
            }
        }

        public static string MailjetPrivateKey
        {
            get
            {
                if (Configuration["Mailjet:PrivateKey"] == null || string.IsNullOrWhiteSpace(Configuration["Mailjet:PrivateKey"]))
                    throw new Exception("There is no mailjet private key defined.");
                return Configuration["Mailjet:PrivateKey"];
            }
        }

         public static string MailjetReceiverEmail
        {
            get
            {
                if (Configuration["Mailjet:ReceiverEmail"] == null || string.IsNullOrWhiteSpace(Configuration["Mailjet:ReceiverEmail"]))
                    throw new Exception("There is no mailjet receiver email defined.");
                return Configuration["Mailjet:ReceiverEmail"];
            }
        }

          public static string MailjetSystemEmail
        {
            get
            {
                if (Configuration["Mailjet:SystemEmail"] == null || string.IsNullOrWhiteSpace(Configuration["Mailjet:SystemEmail"]))
                    throw new Exception("There is no mailjet system email defined.");
                return Configuration["Mailjet:SystemEmail"];
            }
        }
    }
}



