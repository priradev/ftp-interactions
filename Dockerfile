FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env

COPY . ./
RUN dotnet publish ./PriRa.GitHub.Actions.Ftp/PriRa.GitHub.Actions.Ftp.csproj -c Release -o out --no-self-contained 

LABEL maintainer="PriRa"
LABEL repository="https://github.com/priradev/ftp-interactions"
LABEL homepage="https://github.com/priradev/ftp-interactions/"

LABEL com.github.actions.name="FTP Interaction to upload and delete files"
LABEL com.github.actions.description=".NET-based GitHub Action to interact with a FTP server."
# https://docs.github.com/actions/creating-actions/metadata-syntax-for-github-actions#branding
LABEL com.github.actions.icon="arrow-up-circle"
LABEL com.github.actions.color="white" 

FROM mcr.microsoft.com/dotnet/runtime:5.0
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "/PriRa.GitHub.Actions.Ftp.dll"]