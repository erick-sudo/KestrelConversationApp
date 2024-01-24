# ConversationApp

# How to run the project in your local  
First of all, please make sure that the docker is running on your device.

### 1- Clone the repository using powershell or terminal
```
 git clone https://github.com/Conversation-App/conversation-app-backend
```

### 2- Navigate to the API Directory
```
 cd conversation-app-backend
```

### 3- Run the docker compose file
```
 docker compose up -d
```

### 4- Wait until images are pulled and containers are initiated inside docker 
After completion, this is what you should see in docker 

![docker](https://github.com/Conversation-App/conversation-app-backend/assets/106915107/b6344412-2059-40b3-bf4c-430ed1fddb0b)

### 5- How to open email service UI
```
 http://localhost:1080
```

### 6- How to open the aplication in swagger
- With HTTPS
```
 https://localhost:8081/swagger/index.html
```
- With HTTP
```
 http://localhost:8080/swagger/index.html
```

### 7- How to send request using Postman (or other applications)
- With HTTPS
```
 https://localhost:8081
```
- With HTTP
```
 http://localhost:8080
```
 
