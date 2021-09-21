## Pull Request ##

The pull request data model is a table that holds the general data about a pull list. It holds the expiration of the list, the priority of the list, the customerId, whether the list has been pulled and whether it is finalized. Individual items in the pull list are stored by FKey association to the PullListId.


### GET api/PullRequest ###

This requires a bearer token and user auth.  This will return all active pull lists (owner, priority, pulled, etc)


### GET api/PullRequest/customerId={customerId(int)} ###

This requires a bearer token. This will return the pull list associated to a particular user id

### GET api/PullRequest/isPriority={isPriority(bool)} ###

Requires a bearer token and authorized user. This will return prioritized pull lists.  The intention for this functionality is kiosk orders and allows staff to pull these request first.

### PUT api/PullRequest ###

Requires a bearer token. This will allow an indvidual pull request to be updated.  It accepts the model.

Ex:
        
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

Ex:
```
{
  "PullRequestId": 12345,
  "IsPulled": false,
  "IsFinalized": false,
  "IsPriority":false,
  "TransactionAmount": 0
}
```






    
