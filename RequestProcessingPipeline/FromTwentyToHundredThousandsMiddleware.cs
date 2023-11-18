using System;

namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredThousandsMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredThousandsMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Session.SetString("number","");
            string? token = context.Request.Query["number"];
            try
            {
                if (token == null)
                {
                    throw new Exception();
                }
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number < 20000)
                {
                    await _next.Invoke(context);
                }
                else if (number > 100000)
                {
                    await context.Response.WriteAsync("Number greater than one hundred thousands");
                }
                else if (number == 100000)
                {
                    await context.Response.WriteAsync("Your number is one hundred thousands");
                }
                else
                {
                    context.Session.SetString("number", "");
                    int num = Convert.ToInt32(token.Substring(0, 1));
                    string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    context.Session.SetString("number", Numbers[num - 2]);
                    await _next.Invoke(context);
                }
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
