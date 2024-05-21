using HandyWMS_ClassLibrary.Utils;
using HandyWMS_Server.Cache.Base.Factory;
using HandyWMS_Server.Utils.DataUtils;
using HandyWMS_Server.Utils.StateUtils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWMS_Server.Models
{
    public class Operator
    {
        public static Operator Instance
        {
            get { return new Operator(); }
        }

        private string LoginProvider = GlobalContext.Configuration.GetSection("SystemConfig:LoginProvider").Value;
        private string TokenName = "UserToken"; //cookie name or session name

        public async Task AddCurrent(string token)
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    new CookieUtil().WriteCookie(TokenName, token);
                    break;

                case "Session":
                    new SessionUtil().WriteSession(TokenName, token);
                    break;

                case "WebApi":
                    OperatorInfo user = await new DataRepository().GetUserByToken(token);
                    if (user != null)
                    {
                        CacheFactory.Cache.SetCache(token, user);
                    }
                    break;

                default:
                    throw new Exception("未找到LoginProvider配置");
            }
        }

        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        public void RemoveCurrent(string apiToken = "")
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    new CookieUtil().RemoveCookie(TokenName);
                    break;

                case "Session":
                    new SessionUtil().RemoveSession(TokenName);
                    break;

                case "WebApi":
                    CacheFactory.Cache.RemoveCache(apiToken);
                    break;

                default:
                    throw new Exception("未找到LoginProvider配置");
            }
        }

        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        public async Task<OperatorInfo> Current(string apiToken = "")
        {
            IHttpContextAccessor hca = GlobalContext.ServiceProvider?.GetService<IHttpContextAccessor>();
            OperatorInfo user = null;
            string token = string.Empty;
            switch (LoginProvider)
            {
                case "Cookie":
                    if (hca.HttpContext != null)
                    {
                        token = new CookieUtil().GetCookie(TokenName);
                    }
                    break;

                case "Session":
                    if (hca.HttpContext != null)
                    {
                        token = new SessionUtil().GetSession(TokenName);
                    }
                    break;

                case "WebApi":
                    token = apiToken;
                    break;
            }
            if (string.IsNullOrEmpty(token))
            {
                return user;
            }
            token = token.Trim('"');
            user = CacheFactory.Cache.GetCache<OperatorInfo>(token);
            if (user == null)
            {
                user = await new DataRepository().GetUserByToken(token);
                if (user != null)
                {
                    CacheFactory.Cache.SetCache(token, user);
                }
            }
            return user;
        }
    }
}
