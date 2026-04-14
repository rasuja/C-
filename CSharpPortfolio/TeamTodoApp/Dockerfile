FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TeamTodoApp.csproj", "./"]
RUN dotnet restore "TeamTodoApp.csproj"
COPY . .
RUN dotnet build "TeamTodoApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamTodoApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 秘密鍵のJSONファイルをコピー
COPY portfolio-todo-app-700b4-firebase-adminsdk-fbsvc-484a03b376.json .

# Cloud Runに環境変数を設定
ENV GOOGLE_APPLICATION_CREDENTIALS="/app/portfolio-todo-app-700b4-firebase-adminsdk-fbsvc-484a03b376.json"

ENTRYPOINT ["dotnet", "TeamTodoApp.dll"]
