using HandyWMS_ClassLibrary.Utils.JsonUtils;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HandyWMS_Server.Models.Base
{
    public class BaseModifyModel : BaseCreateModel
    {/// <summary>
     /// 数据更新版本，控制并发
     /// </summary>
        public int? Version { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        [Description("修改时间")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public long? ModifierId { get; set; }

        public async Task Modify()
        {
            this.Version = 0;
            this.ModifyTime = DateTime.Now;

            if (this.ModifierId == null)
            {
                OperatorInfo user = await Operator.Instance.Current();
                if (user != null)
                {
                    this.ModifierId = user.UserId;
                }
                else
                {
                    if (this.ModifierId == null)
                    {
                        this.ModifierId = 0;
                    }
                }
            }
        }
    }
}
