namespace RequestProcessingPipeline
{
    public static class FromOneToTenThousandsExtensions
    {
        public static IApplicationBuilder UseFromOneToTenThousands(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromOneToTenThousandsMiddleware>();
        }
    }
}
