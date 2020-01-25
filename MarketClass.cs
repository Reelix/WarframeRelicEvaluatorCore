using System;

namespace WarframeRelicEvaluatorCore
{
    public class Rootobject
    {
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public Order[] orders { get; set; }
    }

    public class Order
    {
        public User user { get; set; }
        public DateTime Last_update { get; set; }
        public string platform { get; set; }
        public DateTime Creation_date { get; set; }
        public string Order_type { get; set; }
        public int Quantity { get; set; }
        public string Region { get; set; }
        public float Platinum { get; set; } // float for bulk trades according to dev
        public bool Visible { get; set; }
        public string Id { get; set; }
    }

    public class User
    {
        public string Ingame_name { get; set; }
        public DateTime? Last_seen { get; set; } // Can be null - Wtf?
        public int Reputation_bonus { get; set; }
        public int Reputation { get; set; }
        public string Region { get; set; }
        public string Avatar { get; set; }
        public string Status { get; set; }
        public string Id { get; set; }
    }

}
