FROM node:alpine
WORKDIR /usr/src/app

RUN npm install @angular/cli -g
RUN npm install http-server -g

COPY package*.json ./
RUN npm install

COPY . .

RUN ng build --prod

ENTRYPOINT [ "http-server", "./dist/GolfGearUi/", "-p", "3000"]
