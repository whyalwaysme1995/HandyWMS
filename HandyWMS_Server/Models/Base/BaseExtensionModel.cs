using Newtonsoft.Json;

namespace HandyWMS_Server.Models.Base
{
    public class BaseExtensionModel : BaseModifyModel
    {
        /// <summary>
        /// 是否删除 1是，0否
        /// </summary>
        [JsonIgnore]
        public int? IsDelete { get; set; }

        public new async Task Create()
        {
            this.IsDelete = 0;

            await base.Create();

            await base.Modify();
        }

        public new async Task Modify()
        {
            await base.Modify();
        }
    }
}
