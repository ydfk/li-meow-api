//-----------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 16:34:21</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.User;
using System;

namespace LiMeowApi.Schema.Base
{
    public class BaseModel
    {
        public virtual string Id { get; set; }

        public virtual bool DataStatus { get; set; }

        public virtual DateTime CreateAt { get; set; }

        public virtual DateTime UpdateAt { get; set; }

        public virtual SimpleUserModel CreateBy { get; set; }

        public virtual SimpleUserModel UpdateBy { get; set; }
    }
}