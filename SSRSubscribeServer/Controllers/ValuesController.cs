using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSRSubscribeServer.Model;
using System.Text;

namespace SSRSubscribeServer.Controllers
{
    /// <summary>
    /// SSR 订阅链接生成规则
    /// https://github.com/shadowsocksr-backup/shadowsocks-rss/wiki/SSR-QRcode-scheme
    /// https://github.com/shadowsocksr-backup/shadowsocks-rss/wiki/Subscribe-%E6%9C%8D%E5%8A%A1%E5%99%A8%E8%AE%A2%E9%98%85%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {

            var hostList = new[] { "hk1.freessr.com", "hk2.freessr.com", "hk3.freessr.com" };
            const int port = 443;
            const string password = "123456";
            const string method = "aes-256-cfb";
            const string protocol = "auth_aes128_md5";
            const string protocolparam = "1024:Wv6389yvGb";
            const string obfs = "tls1.2_ticket_auth";
            const string @group = "FreeSSR";
            var subscription = string.Empty;
            foreach (var server in hostList)
            {
                //ssr://base64(host:port:protocol:method:obfs:base64pass/?obfsparam=base64param&protoparam=base64param&remarks=base64remarks&group=base64group)
                var ssr = $"ssr://{Base64Url.Base64Encode($"{server}:{port}:{protocol}:{method}:{obfs}:{Base64Url.Base64Encode(password)}/?obfsparam={null}&protoparam={Base64Url.Base64Encode(protocolparam)}&remarks={Base64Url.Base64Encode(server)}&group={Base64Url.Base64Encode(group)}")}";
                subscription += $"{ssr}\r\n";
            }
            var bytes = Encoding.UTF8.GetBytes(subscription);
            return Convert.ToBase64String(bytes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
