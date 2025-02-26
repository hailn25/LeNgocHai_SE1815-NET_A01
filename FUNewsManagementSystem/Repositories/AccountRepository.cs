using LeNgocHaiMVC.DAO;
using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountDAO _accountDAO;
        public AccountRepository(IAccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public IEnumerable<SystemAccount> GetAllAccount()
        {
            return _accountDAO.GetAllAccount();
        }

        public SystemAccount GetAccountById(short id)
        {
            return _accountDAO.GetAccountById(id);
        }

        public void AddAccount(SystemAccount account)
        {
            _accountDAO.AddAccount(account);
        }

        public void UpdateAccount(SystemAccount account)
        {
            _accountDAO.UpdateAccount(account);
        }

        public void DeleteAccount(short id)
        {
            _accountDAO.DeleteAccount(id);
        }
        public async Task<SystemAccount?> GetByEmailAndPassword(string email, string password)
        {
            return await _accountDAO.GetByEmailAndPassword(email, password);
        }

      


    }
}
