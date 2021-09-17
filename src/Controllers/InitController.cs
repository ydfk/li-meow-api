//-----------------------------------------------------------------------
// <copyright file="TestController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>2021/9/1 14:07:44</date>
//-----------------------------------------------------------------------

using LiMeowApi.Extension;
using LiMeowApi.Schema.App;
using LiMeowApi.Schema.Code;
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
        private readonly ICodeService _codeService;

        public InitController(IUserService userService, ICodeService codeService)
        {
            _userService = userService;
            _codeService = codeService;
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

        [HttpPost("AccountCategory")]
        public async Task<bool> InitAccountCategory()
        {
            var codeType = await _codeService.SaveOrUpdateCodeType(new CodeTypeModel
            {
                Id = AppConstant.InComeCodeTypeId,
                Name = "账单收入分类"
            }, UserContext);
            await _codeService.DeleteCodeByCodeTypeId(codeType.Id, true, UserContext);
            await _codeService.SaveOrUpdateCode(new CodeModel
            {
                Name = "支钱",
                CodeTypeId = codeType.Id,
                OrderIndex = 0,
            }, UserContext);

            var expenditureCodeType = await _codeService.SaveOrUpdateCodeType(new CodeTypeModel
            {
                Id = AppConstant.ExpenditureCodeTypeId,
                Name = "账单支出分类"
            }, UserContext);
            await _codeService.DeleteCodeByCodeTypeId(expenditureCodeType.Id, true, UserContext);
            await _codeService.SaveOrUpdateCode(new CodeModel
            {
                Name = "办公用品",
                CodeTypeId = expenditureCodeType.Id,
                OrderIndex = 0,
            }, UserContext);
            await _codeService.SaveOrUpdateCode(new CodeModel
            {
                Name = "办公杂费",
                CodeTypeId = expenditureCodeType.Id,
                OrderIndex = 10,
            }, UserContext);
            await _codeService.SaveOrUpdateCode(new CodeModel
            {
                Name = "其他",
                CodeTypeId = expenditureCodeType.Id,
                OrderIndex = 30,
            }, UserContext);

            return true;
        }
    }
}