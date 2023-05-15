using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Tests
{
    internal static class ResponseAssert
    {
        public static void Assert200OK(this RestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        public static void AssertIs404NotFound(this RestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
        
    }
}
