FROM node:alpine as build
WORKDIR /app

COPY . ./

RUN npm install && npm run build

FROM nginx as runtime

COPY --from=build /app/build /usr/share/nginx/html
COPY ["./conf/default.conf","/etc/nginx/conf.d/default.conf"]