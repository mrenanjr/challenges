FROM node:latest as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./TemplateS.Client/TemplateS.Client/ .
RUN npm install && npm run build

FROM nginx as runtime
COPY --from=build /app/dist/template-s.client /usr/share/nginx/html
COPY ["./TemplateS.Client/TemplateS.Client/conf/default.conf","/etc/nginx/conf.d/default.conf"]
