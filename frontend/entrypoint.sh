#!/bin/sh
sed -i "s|PLACEHOLDER_API_URL|${API_URL}|g" /usr/share/nginx/html/browser/*.js
exec nginx -g 'daemon off;'