using System.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication2.Repositories;

public interface ISystemRepository
{
    public Task<int> RegisterSubscriptionInDatabaseAsync(int idUser, int idService, float leftToPay);

}

public class SystemRepository : ISystemRepository
{
    private readonly IConfiguration _configuration;

    public SystemRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    public async Task<int> RegisterSubscriptionInDatabaseAsync(int idUser, int idService, float leftToPay)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();
        
        try
        {   // check if lesft to pay right
            if (leftToPay < 0) return (int)leftToPay;

            // check if service exists
            var queryCheckIfServiceExists = "SELECT COUNT(*) FROM Service WHERE IdService = idService";
            await using var commandTP1 = new SqlCommand(queryCheckIfServiceExists, connection);
            commandTP1.Transaction = (SqlTransaction)transaction;

            var countTP1 = await commandTP1.ExecuteScalarAsync();
            
            if ((int)countTP1 <=0) return 404;
            
            //check if user exists
            var queryCheckIfUserExists = "SELECT COUNT(*) FROM User WHERE IdUser = idUser";
            await using var commandTP2 = new SqlCommand(queryCheckIfUserExists, connection);
            commandTP2.Transaction = (SqlTransaction)transaction;

            var countTP2 = await commandTP2.ExecuteScalarAsync();

            if ((int)countTP2 <= 0) return 404;
            
            //check if user is already subscriber

            var queryCheckIfIsSubscriber =
                "SELECT COUNT(*) FROM Subscription WHERE IdUser = idUser AND IdService = idService";
            await using var commandTP3 = new SqlCommand(queryCheckIfIsSubscriber, connection);
            commandTP3.Transaction = (SqlTransaction)transaction;

            var countTP3 = await commandTP3.ExecuteScalarAsync();
            
            if ((int)countTP3 > 0) return 404;
            
            // get subscription count
            var queryGetSubCount = "SELECT COUNT(*) FROM Subscription";
            await using var commandTP4 = new SqlCommand(queryGetSubCount, connection);
            commandTP4.Transaction = (SqlTransaction)transaction;

            var countTP4 = await commandTP2.ExecuteScalarAsync();

            //create subscription
            var queryCreateSub = "INSERT INTO Subsciption VALUES((countTP4+1),idUser, idService)";
            await using var commandTP5 = new SqlCommand(queryCreateSub, connection);
            commandTP5.Transaction = (SqlTransaction)transaction;

            //get commitment count
            var queryGetComCount = "SELECT COUNT(*) FROM Commitment";
            await using var commandTP6 = new SqlCommand(queryGetComCount, connection);
            commandTP6.Transaction = (SqlTransaction)transaction;
            
            //create Commitment
            DateTime today = DateTime.Today;
            DateTime weekAfter = today.AddDays(7);
            var queryCreaateComm = "INSERT INTO Commitment VALUES((countTP6),weekAfter, leftToPay,(countTP4+1))";
            await using var commandTP7 = new SqlCommand(queryCheckIfUserExists, connection);
            commandTP7.Transaction = (SqlTransaction)transaction;
            return 200;
            
        }
        catch
        {
            await transaction.RollbackAsync();
            return 409;
        }
    }
}

