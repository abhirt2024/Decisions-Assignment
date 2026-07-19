# Stage 1: Build the React App
FROM node:20-alpine AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the code and build the project
COPY . .
RUN npm run build

# Stage 2: Serve the App with Nginx
FROM nginx:alpine AS final

# Copy the built files from the previous stage
# IMPORTANT: Change 'build' to 'dist' if you are using Vite!
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 80 for the Nginx server
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
