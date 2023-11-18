namespace RequestProcessingPipeline
{
    public class FromOneToTenThousandsMiddleware
    {
        private readonly RequestDelegate _next;

        public FromOneToTenThousandsMiddleware(RequestDelegate next)
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
                if (number < 1000)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    // Отримати рядок, що був записаний в сесію
                    string? sess = context.Session.GetString("number");
                    if (!sess.Contains("thousands"))
                    {
                        // Вибрати третій розряд числа
                        int ind = Convert.ToInt32(token?.ToString().Substring(token.Length - 4, 1));
                        string[] Numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                        string addnum = "", thousandstr;
                        // Якщо третій розряд числа дорівнює нулю, значить кількість тисяч "кругла"
                        // А якщо ні - треба додати третій розряд
                        if (ind != 0)
                        {
                            addnum = Numbers[ind - 1];
                        }
                        // Якщо число менше десятка тисяч, і обрано розряд "1", значить, це одна тисяча
                        if (ind == 1 && number < 10000)
                        {
                            thousandstr = "thousand";
                        }
                        // Якщо ні - значить тисяч багато
                        else
                        {
                            thousandstr = "thousands";
                        }
                        if (sess != null && addnum.Length > 0)
                        {
                            context.Session.SetString("number", $"{sess} {addnum} {thousandstr}");
                        }
                        else if (addnum.Length > 0)
                        {
                            context.Session.SetString("number", $"{addnum} {thousandstr}");
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
