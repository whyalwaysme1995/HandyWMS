using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Client.Objects.SystemManagement.Users
{
    class UserDetail
    {
        public int Index {  get; set; }
        public string Name { get; set; }
        public byte[] ImgPath { get; set; }
        public bool IsSelected { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }

        public UserDetail(int Index, byte[] ImgPath, string Name, bool IsSelected, string Type, string Remark, DateTime CreateTime)
        {
            this.Index = Index;
            this.ImgPath = ImgPath;
            this.Name = Name;
            this.IsSelected = IsSelected;
            this.Type = Type;
            this.Remark = Remark;
            this.CreateTime = CreateTime;
        }
    }
}
