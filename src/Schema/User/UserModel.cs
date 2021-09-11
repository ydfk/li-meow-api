﻿//-----------------------------------------------------------------------
// <copyright file="UserModel.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 16:35:39</date>
//-----------------------------------------------------------------------

using LiMeowApi.Schema.Base;

namespace LiMeowApi.Schema.User
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}