#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AspnetCoreStarter/AspnetCoreStarter.csproj", "AspnetCoreStarter/"]
COPY ["ERPFastTrack.API.Internals/ERPFastTrack.API.Internals.csproj", "ERPFastTrack.API.Internals/"]
COPY ["ERPFastTrack.Abstraction/ERPFastTrack.Abstraction.csproj", "ERPFastTrack.Abstraction/"]
COPY ["ERPFastTrack.Common/ERPFastTrack.Common.csproj", "ERPFastTrack.Common/"]
COPY ["ERPFastTrack.APIModels/ERPFastTrack.APIModels.csproj", "ERPFastTrack.APIModels/"]
COPY ["ERPFastTrack.DBGround/ERPFastTrack.DBGround.csproj", "ERPFastTrack.DBGround/"]
COPY ["ERPFastTrack.DBProcessor/ERPFastTrack.DBProcessor.csproj", "ERPFastTrack.DBProcessor/"]
COPY ["ERPFastTrack.SalesforceProcessor/ERPFastTrack.SalesforceProcessor.csproj", "ERPFastTrack.SalesforceProcessor/"]
COPY ["ERPFastTrack.SourceProcessor/ERPFastTrack.SourceProcessor.csproj", "ERPFastTrack.SourceProcessor/"]
COPY ["ERPFastTrack.LicenseProcessor/ERPFastTrack.LicenseProcessor.csproj", "ERPFastTrack.LicenseProcessor/"]
RUN dotnet restore "./AspnetCoreStarter/AspnetCoreStarter.csproj"
COPY ["AspnetCoreStarter/prepare.sh", "AspnetCoreStarter/"]
COPY . .
WORKDIR "/src/AspnetCoreStarter"
RUN chmod +x prepare.sh
RUN ls
RUN ./prepare.sh
RUN dotnet build "./AspnetCoreStarter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release


# Install ConfuserEx
RUN apt-get update && \
    apt-get install -y wget unzip && \
    wget https://github.com/mkaring/ConfuserEx/releases/download/v1.6.0/ConfuserEx.zip -O /tmp/ConfuserEx.zip && \
    unzip /tmp/ConfuserEx.zip -d /opt/ConfuserEx && \
    rm /tmp/ConfuserEx.zip

# Obfuscate the build output
WORKDIR /app/build
RUN /opt/ConfuserEx/Confuser.CLI.exe -n /src/YourProject/ConfuserEx.crproj -o /app/obfuscated

RUN dotnet publish "./AspnetCoreStarter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/obfuscated .

ENTRYPOINT ["dotnet", "AspnetCoreStarter.dll"]
