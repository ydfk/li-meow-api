//-----------------------------------------------------------------------
// <copyright file="CodeTypeController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 7:13:25 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Code;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    public class CodeTypeController : ApiController
    {
        private readonly ICodeService _codeService;

        public CodeTypeController(ICodeService codeService)
        {
            _codeService = codeService;
        }

        /// <summary>
        /// 初始化codeType
        /// </summary>
        /// <param name="codeType">codeType</param>
        /// <returns>初始化结果</returns>
        [HttpPost]
        public async Task<CodeTypeModel> AddCodeType([FromBody] CodeTypeModel codeType)
        {
            return await _codeService.SaveOrUpdateCodeType(codeType, UserContext);
        }
    }
}
