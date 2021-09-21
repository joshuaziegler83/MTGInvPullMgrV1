## Pull Request Item ##

This is the data model that relates inventory items to a particular parent row on the the PullRequest data model.  The FK Relationship is on the PullRequestId.

### GET /api/PullRequestItem ###

Requires a bearer token parameter. This request returns all items that are in active pull requests.

### GET api/PullRequestItem ###

Requires a bearer token and accepts a json model:

```
{
  "PullRequestId": 0,
  "SKU": 0,
  "Quantity": 0,
  "Price": 0  --price is set from the scryfall api call
}

```

### GET /api/PullRequestItem/pullRequestId={pullRequestId} ###

Requires a bearer token and accepts the int pull request id

```
ex:
https://localhost:44364/api/PullRequestItem/pullRequestId=4

```

### GET /api/PullRequestItem/sku={sku} ###

Requires bearer token auth. Pulls all items that are associated to active pull requests by SKU

```
ex: 
https://localhost:44364/api/PullRequestItem/sku=7

```


