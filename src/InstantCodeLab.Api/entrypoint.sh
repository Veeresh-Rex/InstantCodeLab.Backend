#!/bin/bash

if [ -n "$AppSet" ]; then
  echo "$AppSet" | base64 -d > /app/appsettings.json
fi

dotnet InstantCodeLab.Api.dll
