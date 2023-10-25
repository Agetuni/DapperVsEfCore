

using DapperTest.Model;
using DapperTest.Model.context;
using DapperTest.Model.Repositories;
using Microsoft.EntityFrameworkCore;


ICommandRepository<Person> _entityFramewok = new CommandRepository<Person>(new ApplicationDbContext());
IQureyRepository<long> _dapper = new QureyRepository<long>(@"Server=(localdb)\mssqllocaldb;Database=testDapper;Trusted_Connection=True;");
var efWatch = new System.Diagnostics.Stopwatch(); // stopwatch to calculate entity framework time
var dpWatch = new System.Diagnostics.Stopwatch(); // stopwatch to calculate dapper time 


// seed initial dataof 200,000 records using dapper 
var persons = new List<Person>();
var i = 0;
while (i < 200000)
{
    var person = new Person
    {
        Name = $"alazar{i++}",
        Address = new List<Address> {
            new Address {
                Name="kebele",
                AddressLocation= new List<AddressLocation>
                {
                    new AddressLocation
                    {
                        Name="address location "
                    }
                }

            }
        }

    };
    persons.Add(person);

}
_entityFramewok.AddRange(persons);


// fetching data and measuring time it tooks in milliseconds
var testSenario = 0;
while (testSenario < 50)
{
    efWatch.Start();
    var personsEf = _entityFramewok.Where(x => x.Id > 0, "Address", "Address.AddressLocation").SelectMany(p => p.Address)
                    .SelectMany(a => a.AddressLocation)
                    .Select(al => al.Id).ToList();
    efWatch.Stop();
    var eftime = efWatch.ElapsedMilliseconds;

    dpWatch.Start();
    var personsdapper = _dapper.GetRawQuery(@"SELECT [a0].[Id]
                     FROM [Person] AS [p]
                     INNER JOIN [Address] AS [a] ON [p].[Id] = [a].[PersonId]
                     INNER JOIN [AddressLocation] AS [a0] ON [a].[Id] = [a0].[AddressId]
                     WHERE [p].[Id] > CAST(0 AS bigint) ");
    dpWatch.Stop();


    Console.WriteLine($" time taken by Entity Framework = {efWatch.ElapsedMilliseconds}  time taken by Dapper = {dpWatch.ElapsedMilliseconds}   diference = {efWatch.ElapsedMilliseconds - dpWatch.ElapsedMilliseconds}");


}





