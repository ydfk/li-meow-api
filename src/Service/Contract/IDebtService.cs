//-----------------------------------------------------------------------
// <copyright file="IDebtService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>10/22/2021 3:34:13 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Debt;
using LiMeowApi.Schema.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Contract
{
    public interface IDebtService
    {
        Task<DebtModel> GetById(string id);

        Task<IList<DebtModel>> GetAll();

        Task<bool> DeleteById(string id);

        Task<DebtModel> SaveOrUpdate(DebtModel debt, SimpleUserModel user);
    }
}
