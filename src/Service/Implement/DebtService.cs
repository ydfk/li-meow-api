//-----------------------------------------------------------------------
// <copyright file="DebtService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>10/22/2021 3:34:30 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Entity.Debt;
using LiMeowApi.Extension;
using LiMeowApi.Repository;
using LiMeowApi.Schema.Debt;
using LiMeowApi.Schema.User;
using LiMeowApi.Service.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Implement
{
    public class DebtService : IDebtService
    {
        private readonly IRepository<DebtEntity> _debtRepository;

        public DebtService(IRepository<DebtEntity> debtRepository)
        {
            _debtRepository = debtRepository;
        }

        public async Task<DebtModel> GetById(string id)
        {
            return await _debtRepository.Get<DebtModel>(x => x.Id == id);
        }

        public async Task<IList<DebtModel>> GetAll()
        {
            return await _debtRepository.List<DebtModel>();
        }

        public async Task<bool> DeleteById(string id)
        {
            await _debtRepository.Delete(x => x.Id == id);
            return true;
        }

        public async Task<DebtModel> SaveOrUpdate(DebtModel debt, SimpleUserModel user)
        {
            if (debt.Id.IsNotNullOrEmpty())
            {
                var existDebt = await GetById(debt.Id);
                if (existDebt != null)
                {
                    return await UpdateExistDebt(existDebt, debt, user);
                }
            }

            debt.UpdateBy = user;
            debt.CreateBy = user;
            return await _debtRepository.Save(debt);
        }

        private async Task<DebtModel> UpdateExistDebt(DebtModel existDebt, DebtModel debt, SimpleUserModel user)
        {
            existDebt.Name = debt.Name;
            existDebt.Remark = debt.Remark;
            existDebt.Category = debt.Category;
            existDebt.InstallmentAmount = debt.InstallmentAmount;
            existDebt.InstallmentInterestRate = debt.InstallmentInterestRate;
            existDebt.InstallmentPeriods = debt.InstallmentPeriods;
            existDebt.InstallmentFee = debt.InstallmentFee;
            existDebt.InstallmentDebtDay = debt.InstallmentDebtDay;
            existDebt.BeginDate = debt.BeginDate;

            return await _debtRepository.Update(existDebt);
        }
    }
}
