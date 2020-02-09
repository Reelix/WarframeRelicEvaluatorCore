// Warning: DO NOT fix the case of the variables in this file else the JsonParser breaks

namespace WarframeRelicEvaluatorCore
{
    public class ItemDataRoot
    {
        public ItemDataPayload payload { get; set; }
    }

    public class ItemDataPayload
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string item_name { get; set; }
        public string thumb { get; set; }
        public string id { get; set; }
        public string url_name { get; set; }
    }

}