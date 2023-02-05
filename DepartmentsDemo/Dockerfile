FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DepartmentsDemo/DepartmentsDemo.csproj", "DepartmentsDemo/"]
RUN dotnet restore "DepartmentsDemo/DepartmentsDemo.csproj"
COPY . .
WORKDIR "/src/DepartmentsDemo"
RUN dotnet build "DepartmentsDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DepartmentsDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepartmentsDemo.dll"]
