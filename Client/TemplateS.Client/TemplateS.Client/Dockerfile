FROM node:alpine as builder
WORKDIR /app

COPY package*.json .
RUN npm install
COPY . ./
RUN npm run docker-build

FROM nginx:alpine

COPY --from=builder /app/dist/template-s.client /usr/share/nginx/html

EXPOSE 4200

COPY ["./nginx-custom.conf","/etc/nginx/conf.d/default.conf"]
