using LiMeowApi.Schema;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<AccountModel> GetById(string id)
        {
            return await _accountService.GetAccountById(id);
        }

        [HttpGet("Month")]
        public async Task<IList<AccountModel>> GetByMonth(int year, int month)
        {
            return await _accountService.GetAccountByMonth(year, month, UserContext);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, bool really = false)
        {
            return await _accountService.DeleteAccountById(id, really, UserContext);
        }

        [HttpPut]
        public async Task<AccountModel> Save(AccountModel account)
        {
            return await _accountService.SaveOrUpdateAccount(account, UserContext);
        }

        [HttpPost("{id}")]
        public async Task<AccountModel> Update(string id, [FromBody] AccountModel account)
        {
            account.Id = id;
            return await _accountService.SaveOrUpdateAccount(account, UserContext);
        }

        [HttpPut("Batch")]
        public async Task<dynamic> BatchSaveAccount(List<AccountModel> accounts)
        {
            return await _accountService.BatchSaveAccount(accounts, UserContext);
        }
    }
}
