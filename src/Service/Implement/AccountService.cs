//-----------------------------------------------------------------------
// <copyright file="AccountService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 5:03:29 PM</date>
//-----------------------------------------------------------------------

using LiMeowApi.Repository;
using LiMeowApi.Entity.Account;
using LiMeowApi.Schema;
using LiMeowApi.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace LiMeowApi.Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountEntity> _accountRepository;

        public AccountService(IRepository<AccountEntity> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> DeleteAccountById(string id)
        {
            var account = await _accountRepository.Get<AccountModel>(x => x.Id == id);
            if (account != null && account.DataStatus)
            {
                account.DataStatus = false;
                await _accountRepository.Update(account);
                return true;
            }

            return false;
        }

        public async Task<AccountModel> GetAccountById(string id)
        {
            return await _accountRepository.Get<AccountModel>(x => x.Id == id);
        }

        public async Task<List<AccountModel>> GetAccountByMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = month == 12 ? new DateTime(year, 12, 31): new DateTime(year, month + 1, 1).AddDays(-1);
            var accounts = await _accountRepository.List<AccountModel>(x => x.Date >= startDate && x.Date <= endDate);
            return accounts.OrderBy(x => x.Date).ToList();
        }

        public async Task<AccountModel> SaveOrUpdateAccount(AccountModel account)
        {
            var existAccount = await _accountRepository.Get<AccountModel>(x => x.Id == account.Id);
            if(existAccount != null)
            {
                existAccount.Date = account.Date;
                existAccount.Amount = account.Amount;
                existAccount.Category = account.Category;
                existAccount.Remark = account.Remark;
                existAccount.Type = account.Type;
                existAccount.Receipt = account.Receipt;

                return await _accountRepository.Update(existAccount);
            }
            else
            {
                return await _accountRepository.Save(account);
            }
        }
    }
}
