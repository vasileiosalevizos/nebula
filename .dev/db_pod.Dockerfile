FROM postgres:11.22-bullseye AS nebula_db

# Set environment variables
ENV POSTGRES_PASSWORD yourpassword
ENV POSTGRES_DB yourdbname

# Copy your schema file into the Docker image
COPY your-schema.sql /docker-entrypoint-initdb.d/

# When the container starts, it will execute any .sql file found in /docker-entrypoint-initdb.d/
# This is a feature of the official PostgreSQL image