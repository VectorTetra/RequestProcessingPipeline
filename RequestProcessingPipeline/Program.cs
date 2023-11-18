using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// Все сессии работают поверх объекта IDistributedCache, и 
// ASP.NET Core предоставляет встроенную реализацию IDistributedCache
builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
builder.Services.AddSession();  // Добавляем сервисы сессии
var app = builder.Build();

app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

// Добавляем middleware-компоненты в конвейер обработки запроса.
app.UseFromTwentyToHundredThousands();
app.UseFromTenToTwentyThousands();
app.UseFromOneToTenThousands();
app.UseFromHundredToNineHundred();
app.UseFromTwentyToHundred();
app.UseFromTenToNineteen();
app.UseFromOneToTen();

app.Run();
