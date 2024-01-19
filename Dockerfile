#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Machine_Details_MVC/Machine_Details_MVC.csproj", "Machine_Details_MVC/"]
RUN dotnet restore "Machine_Details_MVC/Machine_Details_MVC.csproj"
COPY . .
WORKDIR "/src/Machine_Details_MVC"
RUN dotnet build "Machine_Details_MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Machine_Details_MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Machine_Details_MVC.dll"]