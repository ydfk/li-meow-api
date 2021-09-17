//-----------------------------------------------------------------------
// <copyright file="CodeEntity.cs" company="QJJS">
//     Copyright QJJS. All rights reserved.
// </copyright>
// <author>liyuhang</author>
// <date>9/17/2021 3:28:34 PM</date>
//-----------------------------------------------------------------------
using LiMeowApi.Entity.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace LiMeowApi.Entity.Code
{
    [BsonDiscriminator("code")]
    [BsonIgnoreExtraElements]
    public class CodeEntity : BaseEntity
    {
        public string Name { get; set; }

        public string ParentId { get; set; }

        public string CodeTypeId { get; set; }

        public string Description { get; set; }

        public int OrderIndex { get; set; }
    }
}
