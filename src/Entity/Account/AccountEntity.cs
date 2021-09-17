//-----------------------------------------------------------------------
// <copyright file="AccountEntity.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 1:37:54 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Entity.Base;
using LiMeowApi.Schema.Account;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LiMeowApi.Entity.Account
{
    [BsonDiscriminator("account")]
    public class AccountEntity : BaseEntity
    {
        /// <summary>
        /// 日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public AccountTypeEnum Type { get; set; }

        /// <summary>
        /// 是否有收据或者发票
        /// </summary>
        public bool Receipt { get; set; }
    }
}
