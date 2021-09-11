//-----------------------------------------------------------------------
// <copyright file="IAccountService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 5:03:12 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Contract
{
    public interface IAccountService
    {
        Task<AccountModel> GetAccountById(string id);

        Task<List<AccountModel>> GetAccountByMonth(int year, int month);

        Task<bool> DeleteAccountById(string id);

        Task<AccountModel> SaveOrUpdateAccount(AccountModel account);
    }
}
