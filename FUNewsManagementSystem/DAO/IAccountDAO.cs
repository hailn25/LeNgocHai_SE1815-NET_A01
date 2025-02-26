using LeNgocHaiMVC.Models;

namespace LeNgocHaiMVC.DAO
{
    public interface IAccountDAO
    {
        IEnumerable<SystemAccount> GetAllAccount();
        SystemAccount GetAccountById(short id);
        void AddAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(short id);
        Task<SystemAccount?> GetByEmailAndPassword(string email, string password);
       
    }
}
