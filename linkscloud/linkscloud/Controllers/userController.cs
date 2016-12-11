﻿using System;
using System.Web;
using System.Web.Http;
using linkscloud.Models;
using System.Web.Script.Serialization;

namespace linkscloud.Controllers
{
    public class userController : ApiController
    {
        [HttpPost]
        public String Post()
        {
            var Request = HttpContext.Current.Request;
            var response = "";

            switch (Request["proc"])
            {
                case "register":
                    response = user.add_user(Request["username"], Request["email"], Request["passkey"]);
                    break;
                case "info":
                    user result = user.info_user(Request["criteria"], Request["identifier"]);
                    result._password = null;
                    response = new JavaScriptSerializer().Serialize(result);
                    break;
                case "login":
                    user data = user.info_user("username", Request["username"]);
                    if(Request["key"] == data._password)
                    {
                        response = "true";
                    }else
                    {
                        response = "false";
                    }
                    break;
                default:
                    response = "Unknown Request";
                    break;
            }

            return response;
        }
    }
}
