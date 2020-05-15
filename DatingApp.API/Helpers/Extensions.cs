using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    // here we do not want to create new instance and in order to use any methods inside it we put static fild
    public static class Extensions 
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error",message); // add additional headers to our response
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error"); // we add this to allow the message to display 
            response.Headers.Add("Access-Control-Allow-Origin","*");  // we add this to allow the message to display
        }
    }
}