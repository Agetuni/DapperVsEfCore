using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
namespace DapperTest.Model.Repositories;
public interface IQureyRepository<T>
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetAll(string whereClause, object parameters = null);
    int CountAll();
    bool Exists(object id);
    bool Exists(string whereClause, object parameters = null);
    T Find(object id);
    T FirstOrDefault(string whereClause, object parameters = null);
    IEnumerable<long> GetRawQuery(string query);
}

public class QureyRepository<T> : IQureyRepository<T>
{
    private readonly IDbConnection _dbConnection;

    public QureyRepository(string connectionString)
    {
        _dbConnection = new SqlConnection(connectionString);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbConnection.Query<T>($"SELECT * FROM {typeof(T).Name}");
    }

    public IEnumerable<T> GetAll(string whereClause, object parameters = null)
    {
        return _dbConnection.Query<T>($"SELECT * FROM {typeof(T).Name} WHERE {whereClause}", parameters);
    }

    public int CountAll()
    {
        return _dbConnection.Query<int>($"SELECT COUNT(*) FROM {typeof(T).Name}").FirstOrDefault();
    }

    public bool Exists(object id)
    {
        return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id }) > 0;
    }

    public bool Exists(string whereClause, object parameters = null)
    {
        return _dbConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM {typeof(T).Name} WHERE {whereClause}", parameters) > 0;
    }

    public T Find(object id)
    {
        return _dbConnection.Query<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id }).FirstOrDefault();
    }

    public T FirstOrDefault(string whereClause, object parameters = null)
    {
        return _dbConnection.Query<T>($"SELECT * FROM {typeof(T).Name} WHERE {whereClause}", parameters).FirstOrDefault();
    }

    public IEnumerable<long> GetRawQuery (string query)
    {
        return _dbConnection.Query<long>(query).ToList();
    }
}

