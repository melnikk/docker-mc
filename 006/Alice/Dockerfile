FROM microsoft/dotnet:2.0-sdk-jessie AS builder

WORKDIR /app

COPY . /app

RUN dotnet publish -c Release --runtime ubuntu-x64 -o out

FROM microsoft/dotnet:2.0-runtime-jessie

WORKDIR /app
COPY --from=builder /app/Alice/out/ ./

EXPOSE 33333

ENTRYPOINT ["./Alice"]
