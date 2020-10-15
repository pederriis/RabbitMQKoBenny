using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CowService.Controllers
{
public class CowController : Controller
{
    private Random r;

    private LoginClient client;

    //private IHttpClientFactory clientFactory;

    //public CowController(IHttpClientFactory clientFactory)
    //{
    //    this.clientFactory = clientFactory;
    //    r = new Random();
    //}
    public CowController(LoginClient client)
    {
        this.client = client;
        r = new Random();
    }


    [HttpGet("/test/")]
    public IActionResult Test()
    {
        var result = client.GetData();

        return Ok("Cow controller: " + result);
    }
}
}
