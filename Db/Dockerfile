FROM mcr.microsoft.com/mssql/server

# Create app directory
WORKDIR /usr/src/app

# Copy initialization script
COPY . /usr/src/app

# Set environment variables
ENV SA_PASSWORD Incorrect!
ENV ACCEPT_EULA Y
ENV MSSQL_DB LPHHospital
ENV ENCRYPT false

# Expose port 1433 

EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
CMD /bin/bash ./entrypoint.sh






