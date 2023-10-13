pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }
    stages{
        stage("Build docker"){
            steps{
                sh "docker compose up --build counter-service"
            }
        }
        stage("Build Swagger"){
            steps{
                echo "Building swagger"
            }
        }
    }
    
}