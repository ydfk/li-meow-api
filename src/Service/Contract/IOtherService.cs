﻿//-----------------------------------------------------------------------
// <copyright file="IOtherService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 11:57:26</date>
//-----------------------------------------------------------------------
using LiMeowApi.Entity.Other;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Contract
{
    public interface IOtherService
    {
        Task SaveException(ExceptionEntity exception);
    }
}