using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoRCollectionManager
{
    public class CollectionManager
    {
        private Dictionary<string, Card> _allCollectibleCards = new Dictionary<string, Card>();
        private Dictionary<string, Card> _allUncollectibleCards = new Dictionary<string, Card>();

        public void InitialiseCards(JArray cardsArray)
        {
            foreach (var card in cardsArray)
            {
                var newCard = new Card();
                newCard.Deserialise((JObject)card);
                if (newCard.Collectible)
                {
                    _allCollectibleCards[newCard.Id] = newCard;
                }
                else
                {
                    _allUncollectibleCards[newCard.Id] = newCard;
                }
            }
        }

        public CollectionManager()
        {

        }
    }
}
