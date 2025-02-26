using LeNgocHaiMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LeNgocHaiMVC.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private readonly FunewsManagementContext _context;

        public AccountDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        // Thêm tài khoản
        public void AddAccount(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        // Cập nhật tài khoản
        public void UpdateAccount(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
        }

        // Xoá tài khoản theo ID
        public void DeleteAccount(short id)
        {
            var account = _context.SystemAccounts.Find(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
        }

        // Lấy tài khoản theo ID
        public SystemAccount? GetAccountById(short id)
        {
            return _context.SystemAccounts.Find(id);
        }

        // Lấy danh sách tất cả tài khoản
        public IEnumerable<SystemAccount> GetAllAccount()
        {
            return _context.SystemAccounts.ToList();
        }

        // Kiểm tra đăng nhập
        public async Task<SystemAccount?> GetByEmailAndPassword(string email, string password)
        {
            return await _context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email && a.AccountPassword == password);
        }
       


    }

}
