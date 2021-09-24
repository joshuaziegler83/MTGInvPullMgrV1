## MTGInvPullMgr ##

## Project Description ##

The MTGInvPullMgr v1 is an inventory management solution for Magic the Gathering trading card game vendors that integrates "pull list" functionality for customers.

This functionality allow a shop to store their inventory in a database and get pricing from a publically available API that provides current market values for cards for sale from various online resellers.

This solution will also allow a user to login and create a "pull list". Users will be able to search the store inventory and place limited time holds on products. If a pull list is active, then item quantities in the pull list will be reflected in inventory searches. For example, if there are 7 Generous Gift non-foil cards in stock, and there are 4 claimed across pull lists, then the available inventory will be displayed as 3. The cards will be deducted from actual inventory once the pull list is finalized, and likewise they will not be counted as claimed inventory in the count if the pull list is expired and unfinalized.

In the current version, the pull list is set to expire 2 hours after creation. Version 2 will allow this to be set and will not begin until the items in the pull list are registered by a store user as pulled.


### API Endpoint Documentation ###

[PullRequest Documentation](./PullRequest.md)

[PullRequestItem Documentation](./PullRequestItem.md)

[Inventory Documentation](./Inventory.md)

[Customer Documentation](./Customer.md)






