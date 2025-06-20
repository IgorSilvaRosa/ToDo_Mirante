# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


# Copia apenas o .csproj e restaura dependências
COPY ["ToDo.API/ToDo.API.csproj", "ToDo.API/"]
RUN dotnet restore "ToDo.API/ToDo.API.csproj"


# Copia todo o restante e compila
COPY . .
WORKDIR "/src/ToDo.API"
RUN dotnet publish "ToDo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ToDo.API.dll"]