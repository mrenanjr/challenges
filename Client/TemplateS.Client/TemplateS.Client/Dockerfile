FROM node:latest as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY . ./
RUN npm install && npm run build

FROM nginx as runtime
COPY --from=build /app/dist/template-s.client /usr/share/nginx/html
COPY ["./conf/default.conf","/etc/nginx/conf.d/default.conf"]
