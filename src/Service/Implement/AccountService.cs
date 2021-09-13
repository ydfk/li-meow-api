//-----------------------------------------------------------------------
// <copyright file="AccountService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 5:03:29 PM</date>
//-----------------------------------------------------------------------

using LiMeowApi.Entity.Account;
using LiMeowApi.Extension;
using LiMeowApi.Repository;
using LiMeowApi.Schema;
using LiMeowApi.Schema.Account;
using LiMeowApi.Schema.User;
using LiMeowApi.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountEntity> _accountRepository;

        public AccountService(IRepository<AccountEntity> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> DeleteAccountById(string id, bool really, SimpleUserModel user)
        {
            var account = await _accountRepository.Get<AccountModel>(x => x.Id == id);
            if (account != null)
            {
                if (really)
                {
                    await _accountRepository.Delete(x => x.Id == id);
                    return true;
                }
                else if (account.DataStatus)
                {
                    account.UpdateBy = user;
                    account.DataStatus = false;
                    await _accountRepository.Update(account);
                    return true;
                }
            }

            return false;
        }

        public async Task<AccountModel> GetAccountById(string id)
        {
            return await _accountRepository.Get<AccountModel>(x => x.Id == id);
        }

        public async Task<List<AccountModel>> GetAccountByMonth(int year, int month, SimpleUserModel user)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = month == 12 ? new DateTime(year, 12, 31) : new DateTime(year, month + 1, 1).AddDays(-1);
            var accounts = await _accountRepository.List<AccountModel>(x => x.DataStatus == true && x.Date >= startDate && x.Date <= endDate && x.CreateBy.Id == user.Id);
            return accounts.OrderBy(x => x.Date).ToList();
        }

        public async Task<AccountModel> SaveOrUpdateAccount(AccountModel account, SimpleUserModel user)
        {
            if (account.Type == AccountTypeEnum.Expenditure && account.Amount > 0)
            {
                account.Amount = 0 - account.Amount;
            }

            if (!account.Id.IsNullOrEmpty())
            {
                var existAccount = await _accountRepository.Get<AccountModel>(x => x.Id == account.Id);
                if (existAccount != null)
                {
                    return await UpdateExistAccount(existAccount, account, user);
                }
            }

            account.UpdateBy = user;
            account.CreateBy = user;
            return await _accountRepository.Save(account);
        }

        public async Task<dynamic> BatchSaveAccount(List<AccountModel> accounts, SimpleUserModel user)
        {
            var saveCount = 0;
            var existAccounts = await _accountRepository.List<AccountModel>();

            foreach (var account in accounts)
            {
                if (account.Amount != 0 && account.Date != DateTime.MinValue && account.Category.IsNotNullOrEmpty())
                {
                    if (account.Type == AccountTypeEnum.Expenditure && account.Amount > 0)
                    {
                        account.Amount = 0 - account.Amount;
                    }

                    //// 同一天相同分类金额一样的情况比较少
                    var existAccount = existAccounts.SingleOrDefault(x => x.Amount == account.Amount && x.Category == account.Category && x.Date == account.Date);

                    if (existAccount == null)
                    {
                        account.CreateBy = user;
                        account.UpdateBy = user;
                        await _accountRepository.Save(account);
                        saveCount++;
                    }
                    else
                    {
                        account.UpdateBy = user;
                        await UpdateExistAccount(existAccount, account, user);
                        saveCount++;
                    }
                }

            }

            return new
            {
                total = accounts.Count,
                saveCount
            };
        }

        private async Task<AccountModel> UpdateExistAccount(AccountModel existAccount, AccountModel account, SimpleUserModel user)
        {
            existAccount.Date = account.Date;
            existAccount.Amount = account.Amount;
            existAccount.Category = account.Category;
            existAccount.Remark = account.Remark;
            existAccount.Type = account.Type;
            existAccount.Receipt = account.Receipt;
            existAccount.UpdateBy = user;

            return await _accountRepository.Update(existAccount);
        }
    }
}
