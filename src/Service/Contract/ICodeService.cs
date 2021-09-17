//-----------------------------------------------------------------------
// <copyright file="ICodeService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 3:57:05 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Code;
using LiMeowApi.Schema.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Contract
{
    public interface ICodeService
    {
        Task<List<CodeModel>> GetCodeByCodeTypeId(params string[] codeTypeIds);
        Task<CodeModel> GetCodeById(string codeId);
        Task<bool> DeleteCodeById(string codeId, bool really, SimpleUserModel user);
        Task<bool> DeleteCodeByCodeTypeId(string codeTypeId, bool really, SimpleUserModel user);
        Task<CodeModel> SaveOrUpdateCode(CodeModel code, SimpleUserModel user);

        Task<CodeTypeModel> SaveOrUpdateCodeType(CodeTypeModel codeType, SimpleUserModel user);
        Task<List<CodeTypeModel>> GetCodeTypeById(params string[] codeTypeIds);
    }
}
