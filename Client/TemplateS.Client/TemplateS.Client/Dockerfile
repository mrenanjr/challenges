FROM node:alpine
WORKDIR /app

EXPOSE 4200

COPY package*.json .
RUN npm install
COPY . ./

CMD ["npm", "run", "docker-start"]
