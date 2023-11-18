namespace RequestProcessingPipeline
{
    public class FromOneToTenMiddleware
    {
        private readonly RequestDelegate _next;

        public FromOneToTenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                string? sess = context.Session.GetString("number");
                int ind = Convert.ToInt32(token?.Substring(token.Length - 1, 1));
                string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                if (ind == 0)
                {
                    await context.Response.WriteAsync($"Your number is {sess}");
                }
                else
                {
                    string addnum = Numbers[ind - 1];
                    if (sess != null)
                    {
                        await context.Response.WriteAsync($"Your number is {sess} {addnum}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"Your number is {addnum}");
                    }
                }

            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
