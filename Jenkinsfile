pipeline{
    agent any
    triggers{
        pollSCM("* * * * *")
    }
    stages{
        stage("Build docker"){
            steps{
                sh "docker compose up counter-service"
            }
        }
    }
}
