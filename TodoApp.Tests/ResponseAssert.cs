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
        public static void Assert200OK(this IRestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        public static void AssertIs404NotFound(this IRestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        public static void AssertIs400BadRequest(this IRestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        public static void AssertIs201Created(this IRestResponse response)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        public static void AssertLocationHeaders(this IRestResponse response, string location)
        {
            var locationHeader = response.Headers.First(h => h.Name == "Location");
            Assert.IsNotNull(locationHeader);
            Assert.AreEqual(location, locationHeader.Value);
        }

    }
}
