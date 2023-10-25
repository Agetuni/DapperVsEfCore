namespace DapperTest.Model;

public class Person
{
    public long Id { get; set; }
    public string Name  { get; set; }
    public ICollection<Address> Address { get; set; }

    public static Person Create(string name)=>  new Person { Name = name };
    
}

public class Address
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long PersonId { get; set; }
    public Person Person { get; set; }
    public ICollection<AddressLocation> AddressLocation { get; set; }
}

public class AddressLocation
{
    public long Id { get; set; }

    public string Name { get; set; }
    public Address Address { get; set; }
    public long AddressId { get; set; }

}
