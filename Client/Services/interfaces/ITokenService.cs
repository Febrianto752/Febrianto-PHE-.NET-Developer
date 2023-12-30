namespace Client.Services.interfaces
{
    public interface ITokenService
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
