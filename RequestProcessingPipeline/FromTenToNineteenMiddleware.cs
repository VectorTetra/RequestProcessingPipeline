namespace RequestProcessingPipeline
{
    public class FromTenToNineteenMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTenToNineteenMiddleware(RequestDelegate next)
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
                if (number < 10)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    int ind = Convert.ToInt32(token?.Substring(token.Length - 2, 2));
                    int ind1 = Convert.ToInt32(ind.ToString().Substring(0, 1));
                    
                    if (ind1 == 1)
                    {
                        string[] Numbers = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                        int ind2 = Convert.ToInt32(ind.ToString().Substring(1, 1));
                        string addnum = Numbers[ind2];

                        string? sess = context.Session.GetString("number");

                        if (token.Length != 2)
                        {
                            //context.Session.SetString("number", $"{sess} {addnum}");
                            //await _next.Invoke(context);
                            await context.Response.WriteAsync($"Your number is {sess} {addnum}");
                        }
                        else
                        {
                            await context.Response.WriteAsync($"Your number is {addnum}");
                        }
                    }
                    else
                    {
                        await _next.Invoke(context);
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
