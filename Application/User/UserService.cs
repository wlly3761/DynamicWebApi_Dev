using ApplicationCommon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.User
{
    [DynamicApiInterface]
    [ServiceRegistry(ServicelLifeCycle = "Transient")]
    public class UserService : IUserService
    {
        private static string _userName;
        public string UserName { get => _userName; }

        public UserService()
        {
            _userName="张三";
        }
        [Authorize(Roles = "admin,client")]
        [HttpGet]
        public string GetUserName(string userName)
        {
            _userName=userName;
            return UserName;
        }
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "client")]
        [HttpGet]
        public string GetUserNameBothAuth(string userName)
        {
            _userName=userName;
            return UserName;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("getToken")]
        public object GetToken(string userName, string pwd)
        {
            double expiredMinute = 1;   //过期时间30分钟
            DateTime expiredTime = DateTime.Now.AddMinutes(expiredMinute);
            //TODO验证用户
            if (userName == "admin" && pwd == "123456")
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(expiredTime).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "client")
            };
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConfig.JwtSetting.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: SystemConfig.JwtSetting.Issuer,
                    audience: SystemConfig.JwtSetting.Audience,
                    claims: claims,
                    expires: expiredTime,
                    signingCredentials: creds);
                string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                var tokenInfo = new
                {
                    Token = token,
                    Expired = expiredTime
                };
                return tokenInfo;

            }
            else
            {
                return new
                {
                    Token = string.Empty,
                    Status = "500",
                    ErrorMsg = "username or password is incorrect."
                };
            }
        }
    }
}
