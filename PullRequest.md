## Pull Request ##

The pull request data model is a table that holds the general data about a pull list. It holds the expiration of the list, the priority of the list, the customerId, whether the list has been pulled and whether it is finalized. Individual items in the pull list are stored by FKey association to the PullListId.


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
"ExpirationDateTime": "2021-09-17T00:08:36.965Z", ---this is not really relevant, it is going to be set when the object is created
"IsPulled": false,
"IsFinalized": false,
"IsPriority": true,
"TransactionAmount": 0,
"Price": 0
}
```
        
        
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






    
