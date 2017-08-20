#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Services.Notifications
dotnet run --no-restore --urls "http://*:10005"