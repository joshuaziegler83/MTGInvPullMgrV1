## Dealer Inventory ##

The dealer inventory model is where all data relevent to maintaining inventory is stored.

DealerInventory Data Model:
```
public class DealerInventory
    {
        
        [Key]
        public int SKU { get; set; }
        [Required]
        public string Name{ get; set; }
        public string ApiObjectURI { get; set; }
        public int CurrentInventory { get; set; }
        public string SetName { get; set; }
        public string Set { get; set; }
        public int CollectorNumber { get; set; }
        public bool IsFoil { get; set; }
        public bool IsVariant { get; set; }
        public string Rarity { get; set; }
        public string Lang { get; set; }

    }
```

### 