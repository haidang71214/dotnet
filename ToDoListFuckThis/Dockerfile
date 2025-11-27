# ==================== BUILD STAGE ====================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy chỉ file csproj trước → tận dụng cache restore
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ sourcea
COPY . ./

# Publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# ==================== RUNTIME STAGE ====================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Tạo user non-root (bảo mật hơn)
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Copy file đã publish
COPY --from=build /app/publish ./

# Port (mày đang dùng Kestrel mặc định)
EXPOSE 8080

# Environment cho production
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "ToDoListFuckThis.dll"]