using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BitsharesAPI.Service;

namespace BitsharesAPI.Controllers
{
    public class CommandsController : ApiController
    {
        public string Get(string id, string paramString)
        {
            BitsharesClientService bscsvc = new BitsharesClientService();
            return bscsvc.GetCommandResponse(id, paramString);
        }
    }
}
