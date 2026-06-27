using Dapper;
using Identity.DTOs;
using Identity.Models;
using System.Data;

namespace Identity.Services;

public class AccountService(IDbConnection db)
{
    public async Task<UserModel?> GetAccount(string account)
    {
        var sql = @"
SELECT
    *
FROM 
    [user]
WHERE 
    account = @account
";
        var parameters = new { account };
        var result = await db.QueryFirstOrDefaultAsync<UserModel>(sql, parameters);
        return result;
    }


    public async Task<SignUpResponse> CreateAccount(string account, string password_hash)
    {
        var sql = @"
INSERT INTO
    [user]
VALUES
    (@account, @password_hash, GETDATE())
";
        var parameters = new
        {
            account,
            password_hash,
        };
        await db.ExecuteAsync(sql, parameters);

        var result = await db.QueryFirstAsync<SignUpResponse>(
            "SELECT * FROM [user] WHERE account = @account ",
            new { account });

        return result;
    }

}
