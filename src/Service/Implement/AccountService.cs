//-----------------------------------------------------------------------
// <copyright file="AccountService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 5:03:29 PM</date>
//-----------------------------------------------------------------------

using FastHttpApi.Repository;
using LiMeowApi.Entity.Account;
using LiMeowApi.Service.Contract;

namespace LiMeowApi.Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountEntity> _accountRepository;

        public AccountService(IRepository<AccountEntity> accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}
