FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# فقط فایل csproj مربوطه رو کپی کن با مسیر درست
COPY AFG_New_passport_Website/AFG_New_passport_Website.csproj ./AFG_New_passport_Website/
RUN dotnet restore ./AFG_New_passport_Website/AFG_New_passport_Website.csproj

# بقیه فایل‌ها رو کپی کن
COPY . .

RUN dotnet publish ./AFG_New_passport_Website/AFG_New_passport_Website.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5000
ENTRYPOINT ["dotnet", "AFG_New_passport_Website.dll"]
