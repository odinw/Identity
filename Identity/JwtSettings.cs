namespace Identity;

/// <summary>
/// Json Web Token 設定
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// 簽發者
    /// </summary>
    public string Issuer { get; set; } = "";
    /// <summary>
    /// 接收方
    /// </summary>
    public string Audience { get; set; } = "";
    /// <summary>
    /// 金鑰
    /// </summary>
    public string Key { get; set; } = "";
    /// <summary>
    /// 到期時間
    /// </summary>
    public int ExpireMinutes { get; set; }
}
