#! /bin/sh

az deployment group create \
  --resource-group rynowak-curpy \
  --template-file template.json \
  --what-if-result-format FullResourcePayloads