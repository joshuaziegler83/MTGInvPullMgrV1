## Pull Request Item ##

This is the data model that relates inventory items to a particular parent row on the the PullRequest data model.  The FK Relationship is on the PullRequestId.
```
public class PullRequestItem
    {
        [Key]
        public int PullRequestItemId{ get; set; }
        [Required, ForeignKey(nameof(PullRequest))]
        public int PullRequestId { get; set; }
        public virtual PullRequest PullRequest { get; set; }
        [Required, ForeignKey(nameof(DealerInventory))]
        public int SKU { get; set; }
        public virtual DealerInventory DealerInventory { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
```
### POST /api/PullRequestItem ###

Requires 2 parameters:
Bearer Token string
model json

Ex Model:
```
{
  "PullRequestId": 5,
  "SKU": 3,
  "Quantity": 1,
  "Price": 0
}
```

**Note:** *The request will return a 500 response if the updated item quantity is greater than the available inventory.*
          *Available inventory is the amount of current actual inventory less the quantity of an SKU within all active pull requests*

### PUT /api/PullRequestItem ###
Requires 2 parameters:
Bearer Token string
Model json

Ex Model:
```
{
  "PullRequestItemId": 12,
  "PullRequestId": 4,
  "SKU": 5,
  "Quantity": ,
  "Price": 0
}
```
**Note:** *The request will return a 500 response if the updated item quantity is greater than the available inventory.*
          *Available inventory is the amount of current actual inventory less the quantity of an SKU within all active pull requests*


### GET /api/PullRequestItem ###

Requires a bearer token parameter. This request returns all items that are in active pull requests.

Ex Return:
```
 {
    "PullRequestItemId": 10,
    "PullRequestId": 4,
    "SKU": 5,
    "Quantity": 3,
    "Price": 0.23
  },
  {
    "PullRequestItemId": 13,
    "PullRequestId": 5,
    "SKU": 5,
    "Quantity": 3,
    "Price": 0.23
  },
  {
    "PullRequestItemId": 15,
    "PullRequestId": 5,
    "SKU": 4,
    "Quantity": 2,
    "Price": 0.15
  }
```
**Notes:** *The price reflected here are pulled from an API call to api.scryfall.com based on whether the card is foil or not. Etched pricing is not yet supported.*

### GET /api/PullRequestItem/pullRequestId={pullRequestId} ###
Retrieves all requested items by the pullrequest Id.
Accepts 2 parameters:
pullRequestId int
Bearer Token string


Ex Return:
```
  {
    "PullRequestItemId": 13,
    "PullRequestId": 5,
    "SKU": 5,
    "Quantity": 3,
    "Price": 0.23
  },
  {
    "PullRequestItemId": 15,
    "PullRequestId": 5,
    "SKU": 4,
    "Quantity": 2,
    "Price": 0.15
  }
```


### GET /api/PullRequestItem/sku={sku} ###

Retrieves all items with a provided SKU that are in active Pull Requests.
Accepts 2 parameters:
Bearer Token string
SKU int

Ex Return: 
```
{
    "PullRequestItemId": 10,
    "PullRequestId": 4,
    "SKU": 5,
    "Quantity": 3,
    "Price": 0.23
  },
  {
    "PullRequestItemId": 13,
    "PullRequestId": 5,
    "SKU": 5,
    "Quantity": 3,
    "Price": 0.23
  }


```


