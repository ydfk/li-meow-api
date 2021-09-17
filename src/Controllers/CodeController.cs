//-----------------------------------------------------------------------
// <copyright file="CodeController.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 5:18:04 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Base;
using LiMeowApi.Schema.Code;
using LiMeowApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiMeowApi.Controllers
{
    public class CodeController : ApiController
    {
        private readonly ICodeService _codeService;

        public CodeController(ICodeService codeService)
        {
            _codeService = codeService;
        }

        /// <summary>
        /// 根据codeTypeId获取code
        /// </summary>
        /// <param name="codeTypeId">codeTypeId</param>
        /// <returns>code</returns>
        [HttpGet]
        public async Task<IList<ItemSourceModel>> GetCodeByCodeTypeId([FromQuery] string codeTypeId)
        {
            var codeList = await _codeService.GetCodeByCodeTypeId(codeTypeId);
            return codeList.Select(x => new ItemSourceModel
            {
                Text = x.Name,
                Value = x.Id,
            }).ToList();
        }

        /// <summary>
        /// 新增code
        /// </summary>
        /// <param name="codes">code</param>
        /// <returns>初始化结果</returns>
        [HttpPost]
        public async Task<dynamic> AddCode([FromQuery] List<CodeModel> codes)
        {
            var codeTypeIds = codes.Select(x => x.CodeTypeId).Distinct().ToArray();
            var codeTypeList = await _codeService.GetCodeTypeById(codeTypeIds);

            if (codeTypeList.Count != codeTypeIds.Length)
            {
                return "存在没有codetype的数据";
            }

            var saveCodes = new List<CodeModel>();

            foreach (var code in codes)
            {
                saveCodes.Add(await _codeService.SaveOrUpdateCode(code, UserContext));
            }

            return saveCodes;
        }
    }
}
