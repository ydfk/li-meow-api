//-----------------------------------------------------------------------
// <copyright file="DebtModel.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>10/22/2021 3:31:39 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Base;
using System;

namespace LiMeowApi.Schema.Debt
{
    public class DebtModel : BaseModel
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public DebtCategoryEnum Category { get; set; }

        /// <summary>
        /// 分期金额
        /// </summary>
        public double InstallmentAmount { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        public double InstallmentInterestRate { get; set; }

        /// <summary>
        /// 分期期数
        /// </summary>
        public int InstallmentPeriods { get; set; }

        /// <summary>
        /// 每期手续费
        /// </summary>
        public double InstallmentFee { get; set; }

        /// <summary>
        /// 每期账单偿还日
        /// </summary>
        public int InstallmentDebtDay { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
    }
}
