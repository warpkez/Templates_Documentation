using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PersonModel;

public class PersonContext : DbContext
{
    // Required settings set to defaults in the absence of valid
    // entries
    readonly IConfiguration? _configuration = null;
    readonly string _connectionstring = "";
    readonly string _databasename = "Default";
    readonly string _profile = "Default";
    readonly string _container = "Default";

    public DbSet<Person> Persons { get; set; }

    // Constructor to configure required settings to access the database server
    // 
    public PersonContext(IConfiguration Config, string Profile, string DatabaseName, string Container)
    {
        if (Config is not null)
        {
            _configuration = Config;
            _databasename = DatabaseName;
            _container = Container;
            _profile = Profile;
            _connectionstring = _configuration.GetConnectionString(_profile);
        }
    }

    // Constructor to configure required settings to access the database server
    //
    public PersonContext(string ConnectionString, string DatabaseName, string Container)
    {
        _connectionstring = ConnectionString;
        _databasename = DatabaseName;
        _container = Container;
    }

    // Configuration for Cosmos DB records
    // _container is the target container in the Database
    // Remove the Model|${GUID} format for the ID/PartitionKey
    // Set the PartitionKey to that of the id
    // Set the unique PrimaryKey for the records
    //
    // These were taken from the CosmosDB and EntityFramework documentation
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Person>()
            .ToContainer(_container)
            .HasNoDiscriminator()
            .HasPartitionKey(o => o.id)
            .HasKey(o => o.id);

    }

    // Using CosmosDB as the database server.
    // This works with hosted and emulated instances
    //
    // These were taken from the CosmosDB and EntityFramework documentation
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(_connectionstring, _databasename);
        base.OnConfiguring(optionsBuilder);
    }
}

// Person Model
public class Person
{
    public Guid id { get; set; } = new Guid();
    public string FirstName { get; set; } = "Bob";
    public string LastName { get; set; } = "Bobinsons";
    public DateOfBirth DOB { get; set; } = new DateOfBirth();
}

// Date of birth model
public class DateOfBirth
{
    public int Day { get; set; } = int.Parse(DateTime.Now.Day.ToString());
    public int Month { get; set; } = int.Parse(DateTime.Now.Month.ToString());
    public int Year { get; set; } = int.Parse(DateTime.Now.Year.ToString());
}