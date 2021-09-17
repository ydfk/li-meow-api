using LiMeowApi.Entity.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace LiMeowApi.Entity.Code
{
    [BsonDiscriminator("code_Type")]
    [BsonIgnoreExtraElements]
    public class CodeTypeEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}
