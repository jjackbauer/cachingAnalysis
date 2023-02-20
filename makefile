pub-client:
	dotnet publish client/caching-client/caching-client.csproj -c Release --sc
run-client:
	client/caching-client/bin/Release/net7.0/linux-x64/publish/caching-client localhost 5000
pub-server:
	dotnet publish server/server-rdb/server-rdb.csproj -c Release --sc
run-server:
	server/server-rdb/bin/Release/net7.0/linux-x64/publish/server-rdb

