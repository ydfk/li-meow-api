//-----------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 5:03:12 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema;
using LiMeowApi.Schema.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Contract
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountById(string id);

        Task<List<AccountModel>> GetAccountByMonth(int year, int month, SimpleUserModel user);

        Task<bool> DeleteAccountById(string id, bool really, SimpleUserModel user);

        Task<AccountModel> SaveOrUpdateAccount(AccountModel account, SimpleUserModel user);

        Task<dynamic> BatchSaveAccount(List<AccountModel> accounts, SimpleUserModel user);

        Task<List<AccountModel>> GetAccount(DateTime startDate, DateTime endDate, SimpleUserModel user);
    }
}
