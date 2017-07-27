#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Services.Notifications
dotnet run --urls "http://*:10005"