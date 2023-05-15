namespace BadHunter.BL.Auth
{
    public interface IEncrypter
    {
        string HashPassword(string password, string salt);
    }
}