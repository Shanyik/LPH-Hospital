#!/bin/bash

# Wait for SQL Server to start
sleep 10s

# Run SQL commands using sqlcmd
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Incorrect! -Q "CREATE DATABASE LPHHospital;"