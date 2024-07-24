#!/bin/sh
# Remove commented lines from appsettings.json
sed '/^\s*\/\//d' appsettings.json > appsettings.tmp && mv appsettings.tmp appsettings.json

# Remove any appsettings.<env>.json files
rm -f appsettings.*.json