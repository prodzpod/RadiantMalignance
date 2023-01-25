using System.Collections.Generic;

namespace RiskyMonkeyBase.LangDynamic.Items
{
    public class ItemModProfile
    {
        public string GUID;
        public string Format;
        public string PickupName;
        public string DescName;
        public List<string> Names;

        public ItemModProfile(string GUID, string Format, string PickupName, string DescName)
        {
            this.GUID = GUID;
            this.Format = Format;
            this.PickupName = PickupName;
            this.DescName = DescName;
            Names = new List<string>();
        }

        public ItemModProfile Add(params string[] Names)
        {
            this.Names.AddRange(Names);
            return this;
        }
    }
}
