FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy everything and build the project
COPY . .
RUN dotnet restore 
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "vlack-api.dll"]