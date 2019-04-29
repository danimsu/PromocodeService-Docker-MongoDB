FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PromocodeService/PromocodeService.csproj", "PromocodeService/"]
RUN dotnet restore "PromocodeService/PromocodeService.csproj"
COPY . .
WORKDIR "/src/PromocodeService"
RUN dotnet build "PromocodeService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PromocodeService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PromocodeService.dll"]