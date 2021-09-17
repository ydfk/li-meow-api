using LiMeowApi.Extension;
using LiMeowApi.Schema;
using LiMeowApi.Schema.App;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly ICodeService _codeService;

        public AccountController(IAccountService accountService, ICodeService codeService)
        {
            _accountService = accountService;
            _codeService = codeService;
        }

        [HttpGet]
        public async Task<IList<AccountModel>> Get(DateTime startDate, DateTime endDate)
        {
            return await _accountService.GetAccount(startDate, endDate, UserContext);
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
            if (accounts.IsNotNullOrEmpty())
            {
                var codeList = await _codeService.GetCodeByCodeTypeId(new string[] { AppConstant.ExpenditureCodeTypeId, AppConstant.InComeCodeTypeId });

                foreach (var account in accounts)
                {
                    var code = codeList.SingleOrDefault(x => x.Name == account.Category);
                    if (code != null)
                    {
                        account.Category = code.Id;
                    }
                }
            }

            return await _accountService.BatchSaveAccount(accounts, UserContext);
        }
    }
}
