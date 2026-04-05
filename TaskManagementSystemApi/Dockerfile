# ===== Build 階段 =====
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 複製 csproj 並還原套件
COPY *.csproj ./
RUN dotnet restore

# 複製所有檔案並 publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ===== Runtime 階段 =====
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# 複製 build 出來的結果
COPY --from=build /app/publish .

# 讓 ASP.NET Core 綁定 Render 指定的 port
ENV ASPNETCORE_URLS=http://+:${PORT}

# 啟動 API
ENTRYPOINT ["dotnet", "TaskManagementSystemApi.dll"]