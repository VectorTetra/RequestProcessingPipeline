namespace RequestProcessingPipeline
{
    public class FromHundredToNineHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromHundredToNineHundredMiddleware(RequestDelegate next)
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
                if (number < 100)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    // Вибрати розряд сотень числа
                    int ind = Convert.ToInt32(token?.ToString().Substring(token.Length - 3, 1));
                    string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    string addnum = "", hundredstr = "";
                    // Якщо третій розряд числа дорівнює нулю, значить кількість тисяч "кругла"
                    // А якщо ні - треба додати третій розряд
                    if (ind != 0)
                    {
                        addnum = Numbers[ind - 1];
                    }
                    // Якщо обрано розряд "1", значить, це одна сотня
                    if (ind == 1)
                    {
                        hundredstr = "hundred";
                    }
                    // Якщо ні - значить тисяч багато (крім випадків, коли в розряді сотень "0")
                    else if (ind != 0)
                    {
                        hundredstr = "hundreds";
                    }
                    // Отримати рядок, що був записаний в сесію
                    string? sess = context.Session.GetString("number");

                    if (sess != null && addnum.Length > 0)
                    {
                        context.Session.SetString("number", $"{sess} {addnum} {hundredstr}");
                    }
                    else if(addnum.Length > 0)
                    {
                        context.Session.SetString("number", $"{addnum} {hundredstr}");
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
