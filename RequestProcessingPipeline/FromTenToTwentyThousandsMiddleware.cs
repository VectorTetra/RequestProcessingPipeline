namespace RequestProcessingPipeline
{
    public class FromTenToTwentyThousandsMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTenToTwentyThousandsMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number < 10000 || number >= 20000)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    int num = Convert.ToInt32(token?.ToString().Substring(token.Length - 4, 1));
                    string[] Numbers = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    context.Session.SetString("number", Numbers[num] + " thousands");
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
