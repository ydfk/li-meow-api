//-----------------------------------------------------------------------
// <copyright file="TestController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 14:07:44</date>
//-----------------------------------------------------------------------

using LiMeowApi.Extension;
using LiMeowApi.Schema.User;
using LiMeowApi.Service.Contract;
using LiMeowApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    /// <summary>
    /// 初始化
    /// </summary>
    [AllowAnonymous]
    public class InitController : ApiController
    {
        private readonly IUserService _userService;

        public InitController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 初始化用户
        /// </summary>
        /// <returns>结果</returns>
        [HttpPost("User")]
        public async Task InitUser()
        {
            var initUsers = AppSettings.InitUsers;
            if (initUsers.IsNotNullOrEmpty())
            {
                var users = JsonUtil.ObjectFromJson<List<UserModel>>(initUsers);
                foreach (var user in users)
                {
                    await _userService.AddUser(user);
                }
            }
        }
    }
}