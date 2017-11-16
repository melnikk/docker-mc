FROM microsoft/dotnet:2.0-sdk-jessie

WORKDIR /app

COPY . /app

RUN dotnet restore
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet"]

CMD ["Alice/out/Alice.dll"]