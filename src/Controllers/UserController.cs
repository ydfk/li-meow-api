﻿//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/11/2021 12:06:36 PM</date>
//-----------------------------------------------------------------------

using LiMeowApi.Schema.User;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>结果</returns>
        [HttpGet("ModifyPassword")]
        public async Task<bool> ModifyPassword(string oldPassword, string newPassword)
        {
            return await _userService.ModifyPassword(UserContext.Id, oldPassword, newPassword);
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns>用户</returns>
        [HttpGet("Current")]
        public async Task<UserModel> GetCurrentUser()
        {
            return await _userService.GetUserById(UserContext.Id);
        }
    }
}
