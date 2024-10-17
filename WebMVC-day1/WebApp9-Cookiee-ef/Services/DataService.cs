using System.Security.Cryptography;
using Humanizer;
using Humanizer.Bytes;
using Microsoft.Extensions.Logging.Console;

namespace WebApp9_cookiee_ef.Services
{

    public interface iData { 
    
      string Name { get; set; }
      int Send(byte[] bytes);
    }



    public class DataService : iData
    {


        public DataService() {
            Console.WriteLine($"+++ ctor DataService: {GetHashCode():x}");
        }    

        public string Name { get ; set; }

        public int Send(byte[] bytes)
        {
            Console.WriteLine($"send data {GetHashCode():x}");
            return bytes.Length;
        }

       
    }
}
