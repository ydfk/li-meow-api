//-----------------------------------------------------------------------
// <copyright file="AccountModel.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 1:46:08 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Base;
using LiMeowApi.Schema.Account;
using System;

namespace LiMeowApi.Schema
{
    public class AccountModel: BaseModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public AccountTypeEnum Type { get; set; }

        /// <summary>
        /// 是否有收据或者发票
        /// </summary>
        public bool Receipt { get; set; }
    }
}
