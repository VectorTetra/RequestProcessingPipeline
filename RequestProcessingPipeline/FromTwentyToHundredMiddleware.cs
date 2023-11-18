namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                
                token = Math.Abs(Convert.ToInt32(token)).ToString();
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if(number < 20)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    int ind = Convert.ToInt32(token?.ToString().Substring(token.Length - 2, 1));
                    string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    if (ind > 1)
                    {
                    string addnum = Numbers[ind - 2];
                    string? sess = context.Session.GetString("number");
                    if (sess != null)
                    {
                        context.Session.SetString("number", $"{sess} {addnum}");
                    }
                    else
                    {
                        context.Session.SetString("number", $"{addnum}");
                    }
                    }
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
