FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Copy everything
COPY . ./
WORKDIR "/source/apps/EasyDo.Web"

# Restore as distinct layers
RUN dotnet restore --use-current-runtime  
# Build and publish a release
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EasyDo.Web.dll"]


# FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /src

# # Applications
# COPY "apps/EasyDo.Web/EasyDo.Web.csproj" "apps/EasyDo.Web/EasyDo.Web.csproj"


# WORKDIR "/src/apps/EasyDo.Web"
# RUN dotnet publish -c Release -o /app 
# # Should be used after .net6 is out of preview for better performance
# # RUN dotnet publish --no-restore -c Release -o /app 

# FROM build AS publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "EasyDo.Web.dll"]