


namespace ElementFitness.Utils
{
    public class Randomizer 
    {
        public static string GenerateRandomName()
        {
            Random randomizer = new Random();
            string filename = "";
            for(int i = 0; i<5; i++)
            {
                filename = filename + randomizer.NextInt64().ToString();
            }
            return filename;
        }

        public static string[] GenerateRandomNames(long numberOfNamesNeeded)
        {
            Random randomizer = new Random();
            string[] filenames = new string[numberOfNamesNeeded];
            string filename;


            for(int i=0; i<numberOfNamesNeeded; i++)
            {
                filename = "";
                for(int j=0; j<5; j++)
                {
                    filename = filename + randomizer.NextInt64().ToString();
                }
                filenames[i] = filename;
            }
            return filenames;
        }
    }
}