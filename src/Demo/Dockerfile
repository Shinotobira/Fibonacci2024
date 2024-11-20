# Étape 1 : Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build
WORKDIR /src
COPY . .
RUN apk add clang gcc lld musl-dev build-base zlib-dev krb5-dev
RUN dotnet publish "Demo.csproj" -c Release -r linux-musl-x64 /p:PublishAot=true /p:PublishTrimmed=true /p:PublishReadyToRun=true -o /publish

# Étape 2 : Image finale
FROM docker.io/library/alpine:latest AS base

# Créer un utilisateur non-root
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app

# Copier les fichiers de l'étape de build
COPY --from=build /publish .

# Donner les droits à l'utilisateur non-root
RUN chown -R appuser:appgroup /app

# Configurer l'environnement
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

# Passer à l'utilisateur non-root
USER appuser

# Définir le point d'entrée
ENTRYPOINT ["/app/Demo"]
