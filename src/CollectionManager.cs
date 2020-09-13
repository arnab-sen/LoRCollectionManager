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
    public class CollectionManager
    {
        #region public properties
        public IEnumerable<Card> AllCollectibleCards => _allCollectibleCards.Select(kvp => kvp.Value);
        public IEnumerable<Card> AllUncollectibleCards => _allUncollectibleCards.Select(kvp => kvp.Value);

        public IEnumerable<CardCodeAndCount> CurrentUserCollection
        {
            get => _currentUserCollection.Select(c => new CardCodeAndCount() { CardCode = c.Key, Count = c.Value });
        }
        public IEnumerable<CardCodeAndCount> DesiredUserCollection
        {
            get => _desiredCollection.Select(c => new CardCodeAndCount() { CardCode = c.Key, Count = c.Value });
        }
        #endregion

        #region private fields
        private Dictionary<string, Card> _allCollectibleCards = new Dictionary<string, Card>();
        private Dictionary<string, Card> _allUncollectibleCards = new Dictionary<string, Card>();
        private Dictionary<string, int> _currentUserCollection = new Dictionary<string, int>();
        private Dictionary<string, int> _desiredCollection = new Dictionary<string, int>();
        #endregion

        public Card GetCardById(string id)
        {
            if (_allCollectibleCards.ContainsKey(id))
            {
                return _allCollectibleCards[id];
            }
            else if (_allUncollectibleCards.ContainsKey(id))
            {
                return _allUncollectibleCards[id];
            }
            else
            {
                return null;
            }
            
        }

        public void AddToAllCards(JArray cardsArray)
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

        /// <summary>
        /// Adds copies of cards from a deck code to an existing collection, up to a certain amount. The collection is not cleared before this operation happens.
        /// </summary>
        /// <param name="code">A code representing a collection of cards (not restricted to just 40-card decks).</param>
        /// <param name="collection">The existing collection to add to.</param>
        /// <param name="maxCopies">The max number of copies of a card allowed in the existing collection.</param>
        public void AddFromCollectionCode(string code, Dictionary<string, int> collection, int maxCopies = 3)
        {
            var cardsAndCounts = LoRDeckEncoder.GetDeckFromCode(code);

            foreach (var cardCodeAndCount in cardsAndCounts)
            {
                collection[cardCodeAndCount.CardCode] = cardCodeAndCount.Count;

                if (collection.ContainsKey(cardCodeAndCount.CardCode))
                {
                    collection[cardCodeAndCount.CardCode] = Math.Min(collection[cardCodeAndCount.CardCode] + cardCodeAndCount.Count, maxCopies);
                }
                else
                {
                    collection[cardCodeAndCount.CardCode] = cardCodeAndCount.Count;
                }
            }
        }

        public void ImportFromCollectionCode(string collectionCode)
        {
            _currentUserCollection.Clear();
            AddFromCollectionCode(collectionCode, _currentUserCollection);
        }

        public void UpdateDesiredCollection(string deckCode)
        {
            AddFromCollectionCode(deckCode, _desiredCollection);

            var newCode = LoRDeckEncoder.GetCodeFromDeck(DesiredUserCollection.ToList());
            UpdateCache("$..DesiredCollectionCode", new JValue(newCode));
        }

        private void UpdateCache(string jsonPath, JToken updatedValue)
        {
            if (File.Exists(@"cache\usercollection.json"))
            {
                var cache = File.ReadAllText(@"cache\usercollection.json");
                if (string.IsNullOrWhiteSpace(cache)) return;

                var obj = JToken.Parse(cache);
                var toUpdate = obj.SelectTokens(jsonPath);

                foreach (var jt in toUpdate)
                {
                    jt.Replace(updatedValue);
                }

                File.WriteAllText(@"cache\usercollection.json", obj.ToString());
            }
        }

        public CollectionManager(string collectionCode = "")
        {
            if (!string.IsNullOrEmpty(collectionCode)) ImportFromCollectionCode(collectionCode);
            if (!File.Exists(@"cache\usercollection.json"))
            {
                var obj = new JObject();
                obj["CurrentCollectionCode"] = collectionCode;
                obj["DesiredCollectionCode"] = "";

                File.WriteAllText(@"cache\usercollection.json", obj.ToString());
            }
        }
    }
}
