# Материалы для мастер-класса по Докеру

### Готовим среду

- https://git-scm.com/
- https://www.docker.com/docker-windows
- https://www.docker.com/docker-mac
- https://code.visualstudio.com/
- https://www.jetbrains.com/rider/

### Launchpad

```bash
git clone https://github.com/vostok/launchpad.git
```

### 001. Создаём Alice

```bash
vostok create -n Alice
dotnet restore
dotnet run
```

### 002. Dockerfile

```dockerfile
FROM microsoft/dotnet:2.0-sdk-jessie
WORKDIR /app
COPY . /app
RUN dotnet restore
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet"]
CMD ["Alice/out/Alice.dll"]

```

### 003. Собираем образ

```bash
docker build .
docker build ./custom/
docker build -t registry.skbkontur.ru/konfur/alice:003 .
docker images
```

### 004. Образ изнутри

```bash
docker run -it --entrypoint=/bin/sh registry.skbkontur.ru/konfur/alice:003
ls -la
```

### 005. Убираем за собой

```bash
docker rmi registry.skbkontur.ru/konfur/alice:003
docker rmi -f `docker images -q`
```

### 006. Dockerfile

```dockerfile
FROM microsoft/dotnet:2.0-sdk-jessie AS builder
# ...
FROM microsoft/dotnet:2.0-runtime-jessie
WORKDIR /app
COPY --from=builder /app/Alice/out/ ./
EXPOSE 33333
ENTRYPOINT ["./Alice"]

```

### 007. Запускаем контейнер

```bash
docker run -it registry.skbkontur.ru/konfur/alice:latest
docker run -it --name alice registry.skbkontur.ru/konfur/alice:latest
docker ps
docker ps -a
docker logs alice
```

### 008. Контейнер изнутри

```bash
docker exec -it alice /bin/bash
```

### 009. Убираем за собой

```bash
docker rm alice
docker rm -fv `docker ps -aq`
```

### 010. Добавляем сервис

```yaml
  alice:
    image: registry.skbkontur.ru/konfur/alice:latest
    depends_on:
      - gate
    ports:
      - 33333:33333
```

### 011. Зовите Bob

```
const string url = "https://google.com";
var request = Vostok.Clusterclient.Model.Request.Post(url);
var cluster = new ClusterClient(
    null,
    config =>
    {
        config.ClusterProvider = new FixedClusterProvider(new Uri(url));
        config.Transport = new VostokHttpTransport(null);
    });
cluster.SendAsync(request);
```