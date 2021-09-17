//-----------------------------------------------------------------------
// <copyright file="CodeEntity.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 3:56:49 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Schema.Base;

namespace LiMeowApi.Schema.Code
{
    public class CodeModel : BaseModel
    {
        public string Name { get; set; }

        public string ParentId { get; set; }

        public string CodeTypeId { get; set; }

        public string Description { get; set; }

        public int OrderIndex { get; set; }
    }
}
