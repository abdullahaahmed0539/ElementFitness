
namespace ElementFitness.Utils
{
    public class Encoder
    {
        public static string ConvertToBase64(MemoryStream stream)
        {
            byte[] bytes = stream.ToArray();;
            return Convert.ToBase64String(bytes);
        }

    }
}