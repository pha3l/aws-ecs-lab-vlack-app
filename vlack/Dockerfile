FROM node:carbon as build-env
WORKDIR /usr/src/app

COPY package*.json ./

RUN npm install

COPY . .

RUN npm run build --production

FROM nginx
COPY --from=build-env /usr/src/app/dist /usr/share/nginx/html
