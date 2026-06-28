using Identity.DTOs;
using Identity.Services;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(AccountService _service, IOptions<JwtSettings> _jwtSettings) : ControllerBase
{
    /// <summary>
    /// 註冊
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> SignUp(SignUpRequest dto)
    {
        // 確認是否已存在
        var account = await _service.GetAccount(dto.Account);
        if (account != null)
            return Conflict(ErrorCode.AccountAlreadyExists.ToString());

        // hash
        var passwordHash = Argon2.Hash(dto.Password);

        // 建立
        var result = await _service.CreateAccount(dto.Account, passwordHash);

        return Ok(result);
    }

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    public async Task<IActionResult> SignIn(SignUpRequest dto)
    {
        var account = await _service.GetAccount(dto.Account);

        // 確認帳號是否存在
        if (account == null)
            return Unauthorized();

        // 驗證帳密
        var result = Argon2.Verify(account.Password_Hash, dto.Password);
        if (!result)
            return Unauthorized();

        // 核發 JWT
        var jwt = GetJWT(account.Id, account.Account);

        return Ok(jwt);
    }

    /// <summary>
    /// 檢查 JWT
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("[action]")]
    public IActionResult CheckJWT()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.Identity?.Name;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            id,
            name,
            role
        });
    }

    string GetJWT(int id, string account)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, account),
            new Claim(ClaimTypes.Role, "NormalUser")
        };

        var setting = _jwtSettings.Value;

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(setting.Key));

        var credential = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: setting.Issuer,
            audience: setting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(setting.ExpireMinutes),
            signingCredentials: credential);

        var jwt = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return jwt;
    }


}

