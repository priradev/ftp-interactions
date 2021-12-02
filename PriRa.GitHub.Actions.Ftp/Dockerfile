FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PriRa.GitHub.Actions.Ftp/PriRa.GitHub.Actions.Ftp.csproj", "PriRa.GitHub.Actions.Ftp/"]
RUN dotnet restore "PriRa.GitHub.Actions.Ftp/PriRa.GitHub.Actions.Ftp.csproj"
COPY . .
WORKDIR "/src/PriRa.GitHub.Actions.Ftp"
RUN dotnet build "PriRa.GitHub.Actions.Ftp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PriRa.GitHub.Actions.Ftp.csproj" -c Release -o /app/publish

LABEL maintainer="Pri Ra"
LABEL repository="https://github.com/priradev/ftp-interactions"
LABEL homepage="https://github.com/priradev/ftp-interactions/"

LABEL com.github.actions.name="FTP Upload and Delete Files"
LABEL com.github.actions.description=".NET-based GitHub Action to interact with a FTP server."
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="arrow-up-circle"
LABEL com.github.actions.color="white" 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriRa.GitHub.Actions.Ftp.dll"]
