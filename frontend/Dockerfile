# Stage 1: Build Angular App
FROM node:20-alpine as build

WORKDIR /app
COPY ./devsu-app/package.json ./devsu-app/package-lock.json ./
RUN npm install
COPY ./devsu-app ./
RUN npm run build --prod

# ... (your Angular build stage) ...

# Stage 2: Serve via NGINX
FROM nginx:alpine

COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/devsu-app /usr/share/nginx/html
