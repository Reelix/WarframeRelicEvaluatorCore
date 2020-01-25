namespace WarframeRelicEvaluatorCore
{

    public class ItemDataRoot
    {
        public ItemPayload payload { get; set; }
    }

    public class ItemPayload
    {
        public ItemData[] items { get; set; }
    }

    public class ItemData
    {
        public string thumb { get; set; }
        public string url_name { get; set; }
        public string item_name { get; set; }
        public string id { get; set; }
    }

}
