#! /bin/sh
az deployment group create \
  --resource-group rynowak-curpy \
  --template-file custom-template.json