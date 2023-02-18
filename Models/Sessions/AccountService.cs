namespace Bitirme_Projesi.Models.Sessions
{
    public class AccountService : IAccountService
    {
        private List<Account> accounts;
        public AccountService()
        {
            accounts = new List<Account>()
            {
            new Account()
            {
                Email= "admin@admin",
                Password="Aaaaaaa1"
            }
            };
        }

        public Account Login(string username, string password)
        {
           return accounts.SingleOrDefault(x=>x.Email==username&& x.Password==password); 
        }
    }
}
