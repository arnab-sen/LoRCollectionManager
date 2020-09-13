using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoRCollectionManager
{
    public class Card
    {
        #region public properties
        public string Id { get; private set; }
        public string Name { get; private set; }
        public bool Collectible { get; private set; } = false;
        public IEnumerable<string> AssociatedCards => _associatedCards?.Select(s => s);
        public IEnumerable<string> AssociatedCardRefs => _associatedCardRefs?.Select(s => s);
        public string Region { get; private set; }
        public string RegionRef { get; private set; }
        public string Attack { get; private set; }
        public string Health { get; private set; }
        public string Cost { get; private set; }
        public string Description { get; private set; }
        public string DescriptionRaw { get; private set; }
        public string LevelUpDescription { get; private set; }
        public string LevelUpDescriptionRaw { get; private set; }
        public string FlavourText { get; private set; }
        public string ArtistName { get; private set; }
        public IEnumerable<string> Keywords => _keywords?.Select(s => s);
        public IEnumerable<string> KeywordRefs => _keywordRefs?.Select(s => s);
        public string SpellSpeed { get; private set; }
        public string SpellSpeedRef { get; private set; }
        public string Rarity { get; private set; }
        public string RarityRef { get; private set; }
        public string Subtype { get; private set; }
        public string SubtypeRef { get; private set; }
        public string Supertype { get; private set; }
        public string Type { get; private set; }
        public string Set { get; private set; }

        #endregion

        #region private fields

        private List<string> _associatedCards;
        private List<string> _associatedCardRefs;
        private List<string> _keywords;
        private List<string> _keywordRefs;

        #endregion
        

        private void Deserialise(string json)
        {
            var obj = JObject.Parse(json);
            Deserialise(obj);
        }

        public void Deserialise(JObject obj)
        {
            Id = obj.GetValue("cardCode").ToString();
            Name = obj.GetValue("name").ToString();
            Collectible = bool.Parse(obj.GetValue("collectible").ToString());
            Region = obj.GetValue("region").ToString();
            RegionRef = obj.GetValue("regionRef").ToString();
            Attack = obj.GetValue("attack").ToString();
            Health = obj.GetValue("health").ToString();
            Cost = obj.GetValue("cost").ToString();
            Description = obj.GetValue("description").ToString();
            DescriptionRaw = obj.GetValue("descriptionRaw").ToString();
            LevelUpDescription = obj.GetValue("levelupDescription").ToString();
            LevelUpDescriptionRaw = obj.GetValue("levelupDescriptionRaw").ToString();
            FlavourText = obj.GetValue("flavorText").ToString();
            ArtistName = obj.GetValue("artistName").ToString();
            SpellSpeed = obj.GetValue("spellSpeed").ToString();
            SpellSpeedRef = obj.GetValue("spellSpeedRef").ToString();
            Rarity = obj.GetValue("rarity").ToString();
            RarityRef = obj.GetValue("rarityRef").ToString();
            Subtype = obj.GetValue("subtype").ToString();
            SubtypeRef = obj.GetValue("subtypes").ToString();
            Supertype = obj.GetValue("supertype").ToString();
            Type = obj.GetValue("type").ToString();
            Set = obj.GetValue("set").ToString();

            _associatedCards = (obj.GetValue("associatedCards") as JArray)?.Select(jt => jt.ToString()).ToList();
            _associatedCardRefs = (obj.GetValue("associatedCardRefs") as JArray)?.Select(jt => jt.ToString()).ToList();
            _keywords = (obj.GetValue("keywords") as JArray)?.Select(jt => jt.ToString()).ToList();
            _keywordRefs = (obj.GetValue("keywordRefs") as JArray)?.Select(jt => jt.ToString()).ToList();

        }

        public override string ToString()
        {
            return Name;
        }

        public Card()
        {

        }
    }
}
