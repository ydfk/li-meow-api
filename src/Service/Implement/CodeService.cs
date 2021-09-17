//-----------------------------------------------------------------------
// <copyright file="CodeService.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 3:59:45 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Entity.Code;
using LiMeowApi.Extension;
using LiMeowApi.Repository;
using LiMeowApi.Schema.Code;
using LiMeowApi.Schema.User;
using LiMeowApi.Service.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiMeowApi.Service.Implement
{
    public class CodeService : ICodeService
    {
        private readonly IRepository<CodeEntity> _codeRepository;
        private readonly IRepository<CodeTypeEntity> _codeTypeRepository;

        public CodeService(IRepository<CodeEntity> codeRepository, IRepository<CodeTypeEntity> codeTypeRepository)
        {
            _codeRepository = codeRepository;
            _codeTypeRepository = codeTypeRepository;
        }

        public async Task<List<CodeModel>> GetCodeByCodeTypeId(params string[] codeTypeIds)
        {
            return await _codeRepository.List<CodeModel>(x => codeTypeIds.Contains(x.CodeTypeId) && x.DataStatus);
        }

        public async Task<CodeModel> GetCodeById(string codeId)
        {
            return await _codeRepository.Get<CodeModel>(x => x.Id == codeId);
        }

        public async Task<bool> DeleteCodeById(string codeId, bool really, SimpleUserModel user)
        {
            var code = await _codeRepository.Get<CodeModel>(x => x.Id == codeId);
            if (code != null)
            {
                if (really)
                {
                    await _codeRepository.Delete(x => x.Id == codeId);
                    return true;
                }
                else if (code.DataStatus)
                {
                    code.UpdateBy = user;
                    code.DataStatus = false;
                    await _codeRepository.Update(code);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> DeleteCodeByCodeTypeId(string codeTypeId, bool really, SimpleUserModel user)
        {
            var codes = await _codeRepository.List<CodeModel>(x => x.CodeTypeId == codeTypeId);
            if (codes.IsNotNullOrEmpty())
            {
                foreach (var code in codes)
                {
                    if (really)
                    {
                        await _codeRepository.Delete(x => x.Id == code.Id);
                    }
                    else if (code.DataStatus)
                    {
                        code.UpdateBy = user;
                        code.DataStatus = false;
                        await _codeRepository.Update(code);
                    }
                }

                return true;
            }

            return false;
        }

        public async Task<CodeModel> SaveOrUpdateCode(CodeModel code, SimpleUserModel user)
        {
            if (!code.Id.IsNullOrEmpty())
            {
                var existCode = await _codeRepository.Get<CodeModel>(x => x.Id == code.Id);
                if (existCode != null)
                {
                    return await UpdateExistCode(existCode, code, user);
                }
            }

            code.UpdateBy = user;
            code.CreateBy = user;
            return await _codeRepository.Save(code);
        }

        public async Task<CodeTypeModel> SaveOrUpdateCodeType(CodeTypeModel codeType, SimpleUserModel user)
        {
            if (!codeType.Id.IsNullOrEmpty())
            {
                var existCodeType = await _codeRepository.Get<CodeTypeModel>(x => x.Id == codeType.Id);
                if (existCodeType != null)
                {
                    existCodeType.Name = codeType.Name;
                    return await _codeTypeRepository.Update(existCodeType);
                }
            }

            codeType.UpdateBy = user;
            codeType.CreateBy = user;
            return await _codeTypeRepository.Save(codeType);
        }

        public async Task<List<CodeTypeModel>> GetCodeTypeById(params string[] codeTypeIds)
        {
            return await _codeTypeRepository.List<CodeTypeModel>(x => codeTypeIds.Contains(x.Id) && x.DataStatus);
        }

        private async Task<CodeModel> UpdateExistCode(CodeModel existCode, CodeModel code, SimpleUserModel user)
        {
            existCode.Name = code.Name;
            existCode.ParentId = code.ParentId;
            existCode.CodeTypeId = code.CodeTypeId;
            existCode.Description = code.Description;
            existCode.OrderIndex = code.OrderIndex;

            existCode.UpdateBy = user;

            return await _codeRepository.Update(existCode);
        }
    }
}
