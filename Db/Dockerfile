# Choose ubuntu version
FROM mcr.microsoft.com/mssql/server

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY . /usr/src/app

# Set environment variables, not have to write them with the docker run command
ENV SA_PASSWORD Incorrect!
ENV ACCEPT_EULA Y
ENV MSSQL_DB LPHHospital
ENV ENCRYPT false

# Expose port 1433 in case accessing from other container

EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
CMD /bin/bash ./entrypoint.sh
