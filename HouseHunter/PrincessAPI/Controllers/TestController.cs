using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrincessAPI.Clarifai;
using PrincessAPI.Infrastructure;
using PrincessAPI.Models;

namespace PrincessAPI.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("testmodel")]
        public List<TestModel> GetTestModel()
        {
            using (var db = new SystemDBContext())
            {
                return db.TestModels.ToList();
            }
        }
    }
}
