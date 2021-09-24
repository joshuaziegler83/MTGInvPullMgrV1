## Pull Request ##

The pull request data model is a table that holds the general data about a pull list. It holds the expiration of the list, the priority of the list, the customerId, whether the list has been pulled and whether it is finalized. Individual items in the pull list are stored by FKey association to the PullListId.
```
public class PullRequest
    {
        [Key]
        public int PullRequestId { get; set; }
        [Required, ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime ExpirationDateTime { get; set; } = new DateTime(2100, 12, 31, 23, 59, 59);
        public bool IsPulled { get; set; }
        public bool IsFinalized { get; set; }
        public bool IsPriority { get; set; }
        public decimal TransactionAmount { get; set; }
    }
```


### GET api/PullRequest ###

This requires a bearer token and user auth.  This will return all active pull lists (owner, priority, pulled, etc)

Ex Return:
```
 {
    "PullRequestId": 4,
    "CustomerId": 1,
    "ExpirationDateTime": "2021-09-21T20:50:18",
    "IsPulled": false,
    "IsFinalized": false,
    "IsPriority": true,
    "TransactionAmount": 0
  },
  {
    "PullRequestId": 5,
    "CustomerId": 2,
    "ExpirationDateTime": "2021-09-21T20:50:29",
    "IsPulled": false,
    "IsFinalized": false,
    "IsPriority": false,
    "TransactionAmount": 0
  }
```


### GET api/PullRequest/customerId={customerId(int)} ###

This requires a bearer token. This will return the pull list associated to a particular user id

Ex Return:
```
{
  "PullRequestId": 5,
  "CustomerId": 2,
  "ExpirationDateTime": "2021-09-21T20:19:29.547",
  "IsPulled": false,
  "IsFinalized": false,
  "IsPriority": false,
  "TransactionAmount": 0
}
```
### GET api/PullRequest/isPriority={isPriority(bool)} ###

Requires a bearer token and authorized user. This will return prioritized pull lists.  The intention for this functionality is kiosk orders and allows staff to pull these request first.

Ex Return:
```
{
  "PullRequestId": 5,
  "CustomerId": 2,
  "ExpirationDateTime": "2021-09-21T20:19:29.547",
  "IsPulled": false,
  "IsFinalized": false,
  "IsPriority": true,
  "TransactionAmount": 0
}
```

### PUT api/PullRequest ###

Requires a bearer token. This will allow an indvidual pull request to be updated.  It accepts the model.

Ex Model:
        
```
{        
"CustomerId": 0,
"ExpirationDateTime": "2021-09-17T00:08:36.965Z",
"IsPulled": false,
"IsFinalized": false,
"IsPriority": true,
"TransactionAmount": 0,
"Price": 0
}
```
**Note:** *The expiration date and time is automatically set to 12-31-2100 235959 when the pull request is created.  The expiration date is reset to DateTime.Now + 2 hours when the Put request is received with the IsPulled parameter set to TRUE.  When a PUT request is received with the IsFinalized parameter set to true, the service will call into the DealerInventoryServices and update the current inventory and set the expiration date and time to DateTime.Now. Also, PullRequestItem Services will be called to get the quantities and prices of the items in the pull request and update the transaction amount on the PullRequest.*
        
### POST api/PullRequest ###

Requires a bearer token.  This allows for the creation of a pull request. Accepts a model.

Ex Model:
```
{
  "PullRequestId": 12345,
  "IsPulled": false,
  "IsFinalized": false,
  "IsPriority":false,
  "TransactionAmount": 0
}
```






    
