# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj first (better layer caching)
COPY /Pragmatic-Programmer/ToDoApi/*.csproj ./ToDoApi/
RUN dotnet restore ./ToDoApi/*.csproj

# copy the rest and publish
COPY /Pragmatic-Programmer/ToDoApi/ ./ToDoApi/
WORKDIR /src/ToDoApi
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# --- runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
# Expose default Kestrel port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ToDoApi.dll"]
