#!/bin/bash

# Start the script to create the DB and user
/docker-entrypoint-initsh.d//configure-db.sh &

# Start SQL Server
/opt/mssql/bin/sqlservr