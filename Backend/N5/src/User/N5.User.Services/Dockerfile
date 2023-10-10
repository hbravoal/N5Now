FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY ./publish/N5.User.Services .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "N5.User.Services.dll", "--run-migration"]