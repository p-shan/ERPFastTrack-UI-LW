# Use a minimal base image
FROM debian:bullseye-slim

# Set the working directory
WORKDIR /app
EXPOSE 80
EXPOSE 443
# Install unzip utility
RUN apt-get update && apt-get install -y unzip libicu-dev && \
    rm -rf /var/lib/apt/lists/*

# Copy the zip file into the container
COPY erpfasttrack-linux-client.zip .

# Unzip the contents
RUN unzip erpfasttrack-linux-client.zip && rm erpfasttrack-linux-client.zip

# Ensure the main binary has execute permissions (replace 'YourApp' with the name of your binary)
RUN chmod +x /app/AspnetCoreStarter


# Set the entry point to run the application
ENTRYPOINT ["/app/AspnetCoreStarter"]