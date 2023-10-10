#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["User/N5.User.Api/N5.User.Api.csproj", "User/N5.User.Api/"]
#RUN dotnet restore "User/N5.User.Api/N5.User.Api.csproj"
#COPY . .
#WORKDIR "/src/User/N5.User.Api"
#RUN dotnet build "N5.User.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "N5.User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "N5.User.Api.dll"]


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./publish/N5.User.Api .
ENV ASPNETCORE_URLS=http://*:3302
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 3302
ENTRYPOINT ["dotnet", "N5.User.Api.dll"]