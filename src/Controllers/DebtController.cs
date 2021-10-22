//-----------------------------------------------------------------------
// <copyright file="DebtController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>10/22/2021 3:36:38 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Debt;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    /// <summary>
    /// 债务
    /// </summary>
    public class DebtController : ApiController
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpGet("{id}")]
        public async Task<DebtModel> GetById(string id)
        {
            return await _debtService.GetById(id);
        }

        [HttpGet]
        public async Task<IList<DebtModel>> GetAll()
        {
            return await _debtService.GetAll();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id)
        {
            return await _debtService.DeleteById(id);
        }

        [HttpPut]
        public async Task<DebtModel> Save(DebtModel debt)
        {
            return await _debtService.SaveOrUpdate(debt, UserContext);
        }

        [HttpPost("{id}")]
        public async Task<DebtModel> Update(string id, [FromBody] DebtModel debt)
        {
            debt.Id = id;
            return await _debtService.SaveOrUpdate(debt, UserContext);
        }
    }
}
