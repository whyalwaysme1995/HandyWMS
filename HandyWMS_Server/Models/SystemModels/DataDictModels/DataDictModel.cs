using HandyWMS_Server.Models.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandyWMS_Server.Models.SystemModels.DataDictModels
{
    [Table("SysDataDict")]
    public class DataDictModel : BaseExtensionModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string DictType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? DictSort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }
    }
}
