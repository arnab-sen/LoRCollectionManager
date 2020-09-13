using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoRDeckCodes;

namespace LoRCollectionManager
{
    public class Application
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Test();
        }

        private Application()
        {

        }

        private void Test()
        {
            var deckCode = "CEBQCAIFF4DQGCJDHBEUWWCZLYCQGBICAMCAKBQAAEAQGCJP";
            var deck = LoRDeckEncoder.GetDeckFromCode(deckCode);

        }
    }
}
