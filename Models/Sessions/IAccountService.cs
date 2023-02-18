namespace Bitirme_Projesi.Models.Sessions
{
    public interface IAccountService
    {
        public Account Login(string username, string password); 
    }
}
