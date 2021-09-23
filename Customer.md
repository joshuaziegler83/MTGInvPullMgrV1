# Customer
---
## public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string Email { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
    }
---
### POST api/Customer	
This requires a bearer token and creates a new customer with a required email address and optional first and last name.

### GET api/Customer
This requires a bearer token. This will get the list of all customers.	

### GET api/Customer?email={email}	
This requires a bearer token and takes an email and returns the data for that specific customer.

Ex:
        
```
{
  "Email": "somecustomer@someisp.com",
  "NameFirst": "Firstname",
  "NameLast": "Lastname"
}
```

### PUT api/Customer
This requires a bearer token and allows you to update the first and last name, but not the email address of the customer.	
