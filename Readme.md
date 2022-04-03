## Sample Cosmos DB application
### Locally hosted Cosmos DB instance

The purpose of this code is to demonstrate how to access a Cosmos DB instance,
write some records, and remove the database.

On the outset it
- Creates the connection
- Ensures that the defined database exists, and will create it if it does not
- Create a predefined number of records saving them to the database in the defined container
- Waits for further input before deleting the database.