using HandyWMS_ClassLibrary.Models;
using HandyWMS_ClassLibrary.Utils.JsonUtils;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HandyWMS_Server.Models
{

    public class BaseCreateModel : BaseModel 
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        [Description("创建时间")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CreatorId { get; set; }

        public new async Task Create()
        {
            base.Create();

            if (CreateTime == null)
            {
                CreateTime = DateTime.Now;
            }

            if (CreatorId == null)
            {
                OperatorInfo user = await Operator.Instance.Current(Token);
                if (user != null)
                {
                    CreatorId = user.UserId;
                }
                else
                {
                    if (CreatorId == null)
                    {
                        CreatorId = 0;
                    }
                }
            }
        }
    }
}
