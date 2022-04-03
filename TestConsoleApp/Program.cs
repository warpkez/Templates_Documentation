using PersonModel;

// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// This would normally be stored in an environment variable
// hosted within an Azure Keyvault.
// Change to appropriate values as required.
string localCosmosDB = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
string dbName = "testing";
string Container = "People";

// Create the database connection
var personContext = new PersonContext(localCosmosDB, dbName, Container);

// If the database does not exist, create it
personContext.Database.EnsureCreated();

// Create 10 records and save them to the database
for (int count = 0; count < 10; count++)
{
    var person = new Person();
    personContext.Persons.Add(person);
    await personContext.SaveChangesAsync();
    Console.WriteLine($"Added generated record {count}");
}

// Wait for the Enter key and then delete the database
Console.ReadLine();
personContext.Database.EnsureDeleted();