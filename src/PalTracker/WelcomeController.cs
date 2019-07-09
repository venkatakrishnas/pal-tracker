using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {

     WelcomeMessage msg ; 

   
     public WelcomeController(WelcomeMessage wm)   
     {
            msg=wm;
     }
       

        [HttpGet]
        public string SayHello() => msg.Message;

    }
}