namespace OprawaObrazowWebApi.Models;

public class AccessTokens
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}