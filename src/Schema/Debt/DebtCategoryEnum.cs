//-----------------------------------------------------------------------
// <copyright file="DebtCategoryEnum.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>10/22/2021 3:05:48 PM</date>
//-----------------------------------------------------------------------
namespace LiMeowApi.Schema.Debt
{
    public enum DebtCategoryEnum
    {
        /// <summary>
        /// 房贷
        /// </summary>
        Mortgage = 0,

        /// <summary>
        /// 车贷
        /// </summary>
        CarLoan = 1,

        /// <summary>
        /// 蚂蚁花呗
        /// </summary>
        HuaBei = 2,

        /// <summary>
        /// 蚂蚁借呗
        /// </summary>
        JieBei = 3,

        /// <summary>
        /// 京东白条
        /// </summary>
        BaiTiao = 4,

        /// <summary>
        /// 京东金条
        /// </summary>
        JinTiao = 5,

        /// <summary>
        /// 信用卡分期
        /// </summary>
        CreditCard = 6,

        /// <summary>
        /// 招商银行-闪电贷
        /// </summary>
        ShaiDianLoan = 7
    }
}
