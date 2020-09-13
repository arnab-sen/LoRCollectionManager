using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoRDeckCodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            // var deckCode = "CEBQCAIFF4DQGCJDHBEUWWCZLYCQGBICAMCAKBQAAEAQGCJP";
            // var deck = LoRDeckEncoder.GetDeckFromCode(deckCode);
            //
            // var collectionCode = "";
            // var collection = LoRDeckEncoder.GetDeckFromCode(collectionCode);

            var cm = new CollectionManager();
            var set1 = File.ReadAllText(@"Resources\CardData\set1-en_us.json");
            var set2 = File.ReadAllText(@"Resources\CardData\set2-en_us.json");
            var set3 = File.ReadAllText(@"Resources\CardData\set3-en_us.json");

            cm.AddToAllCards(JArray.Parse(set1));
            cm.AddToAllCards(JArray.Parse(set2));
            cm.AddToAllCards(JArray.Parse(set3));


            cm.UpdateDesiredCollection("CEBQCAIFF4DQGCJDHBEUWWCZLYCQGBICAMCAKBQAAEAQGCJP");
            cm.UpdateDesiredCollection("CEBQCAIFF4DQGCJDHBEUWWCZLYCQGBICAMCAKBQAAEAQGCJP");
            var cards = cm.DesiredUserCollection.Select(c => cm.GetCardById(c.CardCode)).ToList();
        }
    }
}
