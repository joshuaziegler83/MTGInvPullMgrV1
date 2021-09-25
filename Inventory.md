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

### POST api/Inventory ##
The POST request requires a model and a bearer token. As long as the model state is valid, this will post. 

Ex Model:
```
{
  "SKU": 1, *not required as this is generated auto magically* 
  "Name": "sample string 2",
  "CurrentInventory": 3,
  "ApiObjectURI": "sample string 4",
  "ScryfallUri": "sample string 5",
  "SetName": "sample string 6",
  "Set": "sample string 7",
  "CollectorNumber": 8,
  "IsFoil": true,
  "IsVariant": true,
  "Rarity": "sample string 11",
  "Lang": "sample string 12"
}
```

**Note:** *the api will return a 200 response if successful*

### GET api/Inventory/sku={sku} ###

This GET request requires a SKU integer parameter and a bearer token. This request will return 1 json response as each sku represents one unique inventory item.

Request URL: 
``` 
https://localhost:44364/api/Inventory/sku=2 
```
Ex Response:
```
{
  "SKU": 2,
  "Name": "Counterspell",
  "ApiObjectURI": "https://api.scryfall.com/cards/1920dae4-fb92-4f19-ae4b-eb3276b8dac7",
  "CurrentInventory": 3,
  "AvailableInventory": 3,
  "SetName": "Modern Horizons 2",
  "Set": "mh2",
  "CollectorNumber": 267,
  "IsFoil": false,
  "IsVariant": true,
  "Rarity": "uncommon",
  "Lang": "en"
}
```

### GET /api/Inventory/name={cardName} ###

This request requires a 2 parameters, a card name (fuzzy), and Authorization (Bearer Token). This method will return any 'fuzzy' card name matches.

Request URL:
```
https://localhost:44364/api/Inventory/name=Idol
```

Ex Response:
```
[
  {
    "SKU": 4,
    "Name": "Idol of Endurance",
    "CurrentInventory": 12,
    "AvailableInventory": 9,
    "SetName": "Core Set 2021",
    "Set": "m21",
    "CollectorNumber": 23,
    "IsFoil": false,
    "IsVariant": false,
    "Rarity": "rare",
    "Lang": "en"
  },
  {
    "SKU": 5,
    "Name": "Idol of Endurance",
    "CurrentInventory": 15,
    "AvailableInventory": 8,
    "SetName": "Core Set 2021",
    "Set": "m21",
    "CollectorNumber": 23,
    "IsFoil": true,
    "IsVariant": false,
    "Rarity": "rare",
    "Lang": "en"
  }
]
```

### /api/Inventory/setName={setName} ###

This request requires a 2 parameters, a set name (fuzzy), and Authorization (Bearer Token). The setname search is fuzzy and will return all potential matches

Request URL:
```
https://localhost:44364/api/Inventory/name=Idol
```

Ex Response:
```
[
  {
    "SKU": 2,
    "Name": "Counterspell",
    "CurrentInventory": 3,
    "AvailableInventory": 3,
    "SetName": "Modern Horizons 2",
    "Set": "mh2",
    "CollectorNumber": 267,
    "IsFoil": false,
    "IsVariant": true,
    "Rarity": "uncommon",
    "Lang": "en"
  },
  {
    "SKU": 3,
    "Name": "Counterspell",
    "CurrentInventory": 1,
    "AvailableInventory": 1,
    "SetName": "Modern Horizons 2",
    "Set": "mh2",
    "CollectorNumber": 267,
    "IsFoil": true,
    "IsVariant": true,
    "Rarity": "uncommon",
    "Lang": "en"
  }
]
```

### /api/Inventory/set={set} ###

This request required a fuzzy search term and bearer token. This request is similar to setname, only this is the abbreviated set value from scryfall and TCGPlayer APIs.
Request URL:
```
https://localhost:44364/api/Inventory/set=m2
```

Ex Response:
```
[
  {
    "SKU": 4,
    "Name": "Idol of Endurance",
    "CurrentInventory": 12,
    "AvailableInventory": 9,
    "SetName": "Core Set 2021",
    "Set": "m21",
    "CollectorNumber": 23,
    "IsFoil": false,
    "IsVariant": false,
    "Rarity": "rare",
    "Lang": "en"
  },
  {
    "SKU": 5,
    "Name": "Idol of Endurance",
    "CurrentInventory": 15,
    "AvailableInventory": 8,
    "SetName": "Core Set 2021",
    "Set": "m21",
    "CollectorNumber": 23,
    "IsFoil": true,
    "IsVariant": false,
    "Rarity": "rare",
    "Lang": "en"
  }
]
```
### GET /api/PullRequest/isVariant={isVariant} ###

This request requires a boolean (true) and bearer token. This request will return a list of ALL inventory items that are variants of issued cards.
Request URL:
```
https://localhost:44364/api/PullRequest/isVariant=true
```

Ex Response:
```
[
  {
    "SKU": 2,
    "Name": "Counterspell",
    "CurrentInventory": 3,
    "AvailableInventory": 3,
    "SetName": "Modern Horizons 2",
    "Set": "mh2",
    "CollectorNumber": 267,
    "IsFoil": false,
    "IsVariant": true,
    "Rarity": "uncommon",
    "Lang": "en"
  },
  {
    "SKU": 3,
    "Name": "Counterspell",
    "CurrentInventory": 1,
    "AvailableInventory": 1,
    "SetName": "Modern Horizons 2",
    "Set": "mh2",
    "CollectorNumber": 267,
    "IsFoil": true,
    "IsVariant": true,
    "Rarity": "uncommon",
    "Lang": "en"
  }
]
```

### DELETE /api/Inventory ###

This request requires an integer sku and a bearer token. Authorized users in specific roles will only be allowed to perform this function in future release.

Request URL:
```
https://localhost:44364/api/Inventory?sku=7
```

If the delete is successful, the application will return a 200 response (OK).

