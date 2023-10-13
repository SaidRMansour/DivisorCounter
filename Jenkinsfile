pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }
    stages{
        stage("Build docker"){
            steps{
                sh "/usr/local/bin/docker compose up --build counter-service"
            }
        }
    }
}
