using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// ��� ������ �������� ������ ������� IDistributedCache, � 
// ASP.NET Core ������������� ���������� ���������� IDistributedCache
builder.Services.AddDistributedMemoryCache();// ��������� IDistributedMemoryCache
builder.Services.AddSession();  // ��������� ������� ������
var app = builder.Build();

app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������

// ��������� middleware-���������� � �������� ��������� �������.
app.UseFromTwentyToHundredThousands();
app.UseFromTenToTwentyThousands();
app.UseFromOneToTenThousands();
app.UseFromHundredToNineHundred();
app.UseFromTwentyToHundred();
app.UseFromTenToNineteen();
app.UseFromOneToTen();

app.Run();
